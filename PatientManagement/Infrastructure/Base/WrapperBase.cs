using System;
using System.Collections.Generic;
using Infrastructure.ServiceInterface;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Infrastructure.Base
{
  public  class WrapperBase <T> : NotifyErrorInfoBase, IValidatableTrackingObject, IValidatableObject
    {
        private Dictionary<string, object> _originalValues;
        private List<IValidatableTrackingObject> _trackingObjects;
        public WrapperBase(T wrapper)
        {
            if (wrapper == null)
            {
                throw new ArgumentNullException("wrapper");
            }
            Wrapper = wrapper;
            _originalValues = new Dictionary<string, object>();
            _trackingObjects = new List<IValidatableTrackingObject>();
            InitializeComplexProperties(wrapper);
            InitializeCollectionProperties(wrapper);
            Validate();
        }

        protected virtual void InitializeComplexProperties(T model) { }

        protected virtual void InitializeCollectionProperties(T model) { }

        public T Wrapper { get; private set; }

        public bool IsValid => !HasErrors && _trackingObjects.All(t => t.IsValid);

        public bool IsChanged => _originalValues.Count > 0 || _trackingObjects.Any(t => t.IsChanged);

        public void AcceptChanges()
        {
            _originalValues.Clear();
            foreach(var trackingObject in _trackingObjects)
            {
                trackingObject.AcceptChanges();
            }
            OnPropertyChanged("");
        }

        public void RejectChanges()
        {
           foreach(var originalValueEntry in _originalValues)
            {
                typeof(T).GetProperty(originalValueEntry.Key).SetValue(Wrapper, originalValueEntry.Value);
            }
            _originalValues.Clear();
            foreach(var trackingObject in _trackingObjects)
            {
                trackingObject.RejectChanges();
            }
            Validate();
            OnPropertyChanged("");
        }

        protected bool GetIsChanged(string propertyName)
        {
            return _originalValues.ContainsKey(propertyName);
        }
        protected TValue GetOriginalValue<TValue>(string propertyName)
        {
            return _originalValues.ContainsKey(propertyName) 
                ? (TValue)_originalValues[propertyName] : GetValue<TValue>(propertyName);
        }

        protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Wrapper.GetType().GetProperty(propertyName);
            return (TValue)propertyInfo.GetValue(Wrapper);            
        }

        protected void SetValue<TValue>(TValue newValue, [CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Wrapper.GetType().GetProperty(propertyName);
            var currentValue = propertyInfo.GetValue(Wrapper);
            if(!Equals(currentValue, newValue))
            {
                UpdateOriginalValue(currentValue, newValue, propertyName);
                propertyInfo.SetValue(Wrapper, newValue);
                Validate();
                OnPropertyChanged(propertyName);
                OnPropertyChanged(propertyName + "IsChanged");
            }

        }

        private void UpdateOriginalValue(object currentValue, object newValue, string propertyName)
        {
            if (!_originalValues.ContainsKey(propertyName))
            {
                _originalValues.Add(propertyName, currentValue);
                //OnPropertyChanged("IsChanged");
            }
            else
            {
                if(Equals(_originalValues[propertyName], newValue))
                {
                    _originalValues.Remove(propertyName);
                   // OnPropertyChanged("IsChanged");
                }
            }
            OnPropertyChanged("IsChanged");
        }

        protected void RegisterComplex<TModel>(WrapperBase<TModel> wrapper)
        {
            RegisterTrackingObject(wrapper);
        }

        protected void RegisterCollection<TWrapper, TModel>(
            ChangeTrackingCollection<TWrapper> wrapperCollection, List<TModel> modelCollection)
            where TWrapper : WrapperBase<TModel>
        {
            wrapperCollection.CollectionChanged += (s, e) =>
            {
                modelCollection.Clear();
                modelCollection.AddRange(wrapperCollection.Select(w => w.Wrapper));
                Validate();
            };
            RegisterTrackingObject(wrapperCollection);
        }

        private void Validate()
        {
            ClearErrors();
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            Validator.TryValidateObject(this, context, results, true);

            if (results.Any())
            {
                var propertyNames = results.SelectMany(r => r.MemberNames).Distinct().ToList();

                foreach(var propertyName in propertyNames)
                {
                    Errors[propertyName] = results.Where(r => r.MemberNames.Contains(propertyName)).Select(r => r.ErrorMessage).Distinct().ToList();
                    OnErrorsChanged(propertyName);

                }
            }
            OnPropertyChanged(nameof(IsValid));
        }

        private void RegisterTrackingObject(IValidatableTrackingObject trackingObject)
        {
            if(!_trackingObjects.Contains(trackingObject))
            {
                _trackingObjects.Add(trackingObject);
                trackingObject.PropertyChanged += TrackingObjectPropertyChanged;
            }
        }

        private void TrackingObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(IsChanged))
            {
                OnPropertyChanged(nameof(IsChanged));
            }
            else if(e.PropertyName == nameof(IsValid))
            {
                OnPropertyChanged(nameof(IsValid));
            }
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
