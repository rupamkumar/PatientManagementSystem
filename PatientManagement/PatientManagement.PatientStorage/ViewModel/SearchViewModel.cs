using Infrastructure.Base;
using Infrastructure.Base.Events;
using Infrastructure.DataProvider;
using Infrastructure.ServiceInterface;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using PatientManagement.Model;
using PatientManagement.PatientStorage.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PatientManagement.PatientStorage.ViewModel
{
    public interface ISearchViewModel
    {
        void Load();

        ObservableCollection<Patient> Patients { get; set; }

        SearchItemWrapper SearchItem { get; }

    }
    [Export(typeof(ISearchViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SearchViewModel : ViewModelBase, ISearchViewModel
    {



        public SearchItemWrapper SearchItem
        {
            get
            {                              
                return _searchItem;
            }
            set
            {
                _searchItem = value;
                OnPropertyChanged();
            }
        }

        public INavigationViewModel<Patient> _navigationViewModel { get; private set; }
        private readonly IEventAggregator _eventAgregator;
        private SearchItemWrapper _searchItem;

        public ICommand SearchCommand { get; private set; }
        public ObservableCollection<Patient> Patients { get ; set ; }

        [ImportingConstructor]
        public SearchViewModel(IEventAggregator eventAggregator, INavigationViewModel<Patient> navigationViewModel)
        {
            _eventAgregator = eventAggregator;
            _navigationViewModel = navigationViewModel;
            SearchCommand = new DelegateCommand(OnSearchExecute, OnSearchCanExecute);
            _eventAgregator.GetEvent<WcfEvent<Patient>>().Subscribe(OnPatientSaved);
            Load();
        }

        public void OnPatientSaved(Patient savedPatient)
        {   
            for(int i = 0; i < Patients.Count; i++)
            {
                if(savedPatient.PatientId == Patients[i].PatientId)
                {
                    Patients[i].FirstName = savedPatient.FirstName;
                    Patients[i].LastName = savedPatient.LastName;
                }
            }
            
        }

        public void LoadNavigationView(IEnumerable<Patient> patient)
        {
            _navigationViewModel.Load(patient);
        }

        private bool OnSearchCanExecute()
        {
            if (SearchItem.IsChanged)
            {
                OnSearchExecute();
            }
            else
            {
                if (Patients != null)
                {
                    _eventAgregator.GetEvent<SearchEventBase<IEnumerable<Patient>>>().Publish(Patients);
                }
            }
            return SearchItem.IsChanged;
        }

        private void OnSearchExecute()
        {
            var patients = Patients;
            List<Patient> _patients = new List<Patient>();
            foreach(var patient in patients)
            {
                if(patient.FirstName.ToUpper().Contains(SearchItem.DisplayValue.ToUpper()) || patient.LastName.ToUpper().Contains(SearchItem.DisplayValue.ToUpper()))
                {
                    _patients.Add(patient);
                }
            }
            _eventAgregator.GetEvent<SearchEventBase<IEnumerable<Patient>>>().Publish(_patients);
        }

        public void Load()
        {
            SearchItem = new SearchItemWrapper(new LookupItem { DisplayValue = "" });

            SearchItem.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SearchItem.IsChanged))                    
                {
                    InvalidateCommands();
                }
            };
            InvalidateCommands();
        }

        private void InvalidateCommands()
        {
            ((DelegateCommand)SearchCommand).RaiseCanExecuteChanged();           
        }


    }
}
