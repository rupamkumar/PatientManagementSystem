using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Infrastructure.Base;
using Infrastructure.ServiceInterface;
using Infrastructure.DataProvider;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using PatientManagement.PatientStorage.Events;
using PatientManagement.Model;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using PatientManagement.PatientStorage.DataProvider;
using Infrastructure.Base.Events;
using Microsoft.Practices.Prism.Regions;
using DataAccess.Proxies;
using System.Threading;

namespace PatientManagement.PatientStorage.ViewModel
{

    [Export(typeof(INavigationViewModel<Patient>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NavigationViewModel : INavigationViewModel<Patient>
    {
        [Import]
        public IEventAggregator _eventAggregator;

        [Import]
        public ILookupProvider<Patient> _patientLookupProvider { get; set; }
        //private IClinicalDataServiceClient _proxy;
        private WcfProxy Proxy { get; set; }

        public NavigationViewModel()
        {
        }

        [ImportingConstructor]
        public NavigationViewModel(IEventAggregator eventAggregator, ILookupProvider<Patient> patientLookupProvider)
        {
            _eventAggregator = eventAggregator;
            //Save and Deleted event
            _eventAggregator.GetEvent<WcfEvent<Patient>>().Subscribe(OnPatientSaved);

            _eventAggregator.GetEvent<SearchEventBase<IEnumerable<Patient>>>().Subscribe(OnPatientSearch);
            
            _patientLookupProvider = patientLookupProvider;
            NavigationItems = new ObservableCollection<NavigationItemViewModel>();
        }

        public void OnPatientSearch(IEnumerable<Patient> searchPatient)
        {
            Load(searchPatient);
        }

        public void OnPatientSaved(Patient savedPatient)
        {
            //if(NavigationItems.Count == 0)
            //{
            //    NavigationItems = _patientLookupProvider.
            //}
            var navigationItem = NavigationItems.SingleOrDefault(item => item.PatientId == savedPatient.PatientId);
            if(navigationItem != null)
            {
                navigationItem.DisplayValue = string.Format("{0} {1}", savedPatient.FirstName, savedPatient.LastName);
            }
            else
            {
                //Do nothing
                //Load();
            }
        }

        public ObservableCollection<NavigationItemViewModel> NavigationItems { get; set; }

        
        public void Load()
        {
            var patients = _patientLookupProvider.LoadAsync();
            Load(patients.Result.AsEnumerable());
        }
        //public void Load(Patient patient)
        //{
        //    NavigationItems.Clear();
        //    foreach (var patientLookupItem in _patientLookupProvider.GetLookup(patient))
        //    {
        //        NavigationItems.Add(new NavigationItemViewModel(patientLookupItem.Id, patientLookupItem.DisplayValue, _eventAggregator));
        //    }
        //}

        public void Load(IEnumerable<Patient> patient)
        {
            NavigationItems.Clear();
            var patientLookup = _patientLookupProvider.GetLookup(patient);
            foreach (var patientLookupItem in patientLookup)
            {
                NavigationItems.Add(new NavigationItemViewModel(patientLookupItem.Id, patientLookupItem.DisplayValue, _eventAggregator));
            }
        }

        public void OnObjectSaved(Patient saveObject)
        {
            throw new NotImplementedException();
        }

        public class NavigationItemViewModel : ViewModelBase
        {
            private readonly IEventAggregator _eventAggregator;
            private string _displayValue;

            public NavigationItemViewModel(Guid patientId, string displayValue, IEventAggregator eventAggregator)
            {
                PatientId = patientId;
                DisplayValue = displayValue;
                _eventAggregator = eventAggregator;
                PatientEditViewCommand = new DelegateCommand<object>(PatientEditViewExcute);
               // _proxy = proxy;
            }            

            public Guid PatientId { get; private set; }

            //private WcfProxy _proxy { get; set; }

            public string DisplayValue
            {
                get { return _displayValue; }
                set
                {
                    _displayValue = value;
                    OnPropertyChanged();
                }
            }

            public ICommand PatientEditViewCommand { get; set; }

            private void PatientEditViewExcute(object obj)
            {
                _eventAggregator.GetEvent<PatientEditViewEvent>().Publish(PatientId);
            }

        }
    }
}
