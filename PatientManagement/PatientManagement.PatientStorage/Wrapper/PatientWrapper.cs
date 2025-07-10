using Infrastructure.Base;
using PatientManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.PatientStorage.Wrapper
{
  public  class PatientWrapper : WrapperBase<Patient>
    {
        public PatientWrapper(Patient wrapper) : base(wrapper)
        {

        }

        public Guid PatientId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }
        public Guid IdOriginalValue => GetOriginalValue<Guid>(nameof(PatientId));

        public bool IdIsChanged => GetIsChanged(nameof(PatientId));

        public Guid PatientDepartmentId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }
        public Guid PatientDepartmentIdOriginalValue => GetOriginalValue<Guid>(nameof(PatientDepartmentId));

        public bool PatientDepartmentIdIsChanged => GetIsChanged(nameof(PatientDepartmentId));

       

        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string FirstNameOriginalValue => GetOriginalValue<string>(nameof(FirstName));
        public bool FirstNameIsChanged => GetIsChanged(nameof(FirstName));

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string LastNameOriginalValue => GetOriginalValue<string>(nameof(LastName));
        public bool LastNameIsChanged => GetIsChanged(nameof(LastName));

        public DateTime? BirthDate
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }
        public DateTime? BirthDateOriginalValue => GetOriginalValue<DateTime?>(nameof(BirthDate));
        public bool BirthDateIsChanged => GetIsChanged(nameof(BirthDate));

        public bool IsSerious
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        public bool IsSeriousOriginalValue => GetOriginalValue<bool>(nameof(IsSerious));
        public bool IsSeriousIsChanged => GetIsChanged(nameof(IsSerious));

        public AddressWrapper Address { get; private set; }

        public ChangeTrackingCollection<VisitWrapper> Visits { get; private set; }

        public ObservableCollection<VisitWrapper> DeletedVisit { get; private set; }

        protected override void InitializeComplexProperties(Patient model)
        {
           if(model.Address == null)
            {
                throw new ArgumentException("Address cannot be null");
            }
            Address = new AddressWrapper(model.Address);
            RegisterComplex(Address);
        }
        protected override void InitializeCollectionProperties(Patient model)
        {
            if(model.Visits == null)
            {
                throw new ArgumentException("Notes cannot be null");
            }
            Visits = new ChangeTrackingCollection<VisitWrapper>(model.Visits.Select(v => new VisitWrapper(v)));

            DeletedVisit = new ObservableCollection<VisitWrapper>();
            RegisterCollection(Visits, model.Visits);
        }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(string.IsNullOrEmpty(FirstName))
            {
                yield return new ValidationResult("Firstname is required", new[] { nameof(FirstName) });
            }
            if (string.IsNullOrEmpty(LastName))
            {
                yield return new ValidationResult("Lastname is required", new[] { nameof(LastName) });
            }
            if (IsSerious && Visits.Count == 0)
            {
                yield return new ValidationResult("Patient must have an one visit", new[] {nameof(IsSerious), nameof(Visits) });
            }
        }
    }
}
