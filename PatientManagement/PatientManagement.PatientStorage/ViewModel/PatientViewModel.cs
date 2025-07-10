using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Microsoft.Practices.Prism.Regions;
using PatientManagement.Model;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using PatientManagement.PatientStorage.DataProvider;
using Microsoft.Practices.Prism.Commands;
using Infrastructure.ServiceInterface;
using Microsoft.Practices.Prism.PubSubEvents;
using PatientManagement.PatientStorage.Events;
using Infrastructure.DataProvider;
using System.Windows.Input;
using Infrastructure;
using DataAccess.Contracts;
using DataAccess.Contracts.ServiceInterface;
using DataAccess.Proxies;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Infrastructure.Services;
using RupStoreCore.Common.Contracts;
using RupStoreCore.Common.Core;

namespace PatientManagement.PatientStorage.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
   public class PatientViewModel : ViewModelBase, INavigationAware
    {

        private readonly IEventAggregator _eventAggregator;
       
        
        IWcfProxy _wcfproxy;
        IClinicalDataServiceClient _clinicalServiceClient;
        bool disposed = false;
        private IntPtr _unmanagedPointer;
        
        private ISvcProvider _serviceProvider;
        
        private IPatientEditViewModel _selectedPatientEditViewModel;

        [Import]
        private IPatientEditViewModel _patientEditViewModel;

        
        public ISearchViewModel _searchViewModel { get; private set; }
        
        private readonly IMessageDialogService _messageDialogService;

        private ILookupProvider<Department> _patientDepartmentLookupProvider;
        private ILookupProvider<Doctor> _doctorLookupProvider;
        

        public INavigationViewModel<Patient> _navigationViewModel { get; private set; }
        //public DelegateCommand<string> SelectedItemCommand { get; private set; }

        private EventHandler BroadcastHandler { get; set; }

        
        [ImportingConstructor]
        public PatientViewModel(IEventAggregator eventAggregator,
            INavigationViewModel<Patient> navigationViewModel, IPatientEditViewModel patientEditViewModel, ISearchViewModel searchViewModel,
            ILookupProvider<Department> patientDepartmentLookupProvider, ILookupProvider<Doctor> doctorLookupProvider, IMessageDialogService messageDialogService
           )            
        {
            
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<PatientEditViewEvent>().Subscribe(OnOpenPatientTab);
            //_eventAggregator.GetEvent<WcfEvent<WcfProxy>>().Subscribe(OnWcfProxy);
            //this._patientDataProvider = patientDataProvider;

            //this._doctorDataProvider = doctorDataProvider;
            this._patientDepartmentLookupProvider = patientDepartmentLookupProvider;
            this._doctorLookupProvider = doctorLookupProvider;
            this._messageDialogService = messageDialogService;

            //_testProvider = testProvider;



            Patients = new ObservableCollection<Patient>();
            //SelectedItemCommand = new DelegateCommand<string>(SelectedItem);

            ClosePatientTabCommand = new DelegateCommand<object>(OnClosePatientTabExecute);
            _navigationViewModel = navigationViewModel;

            PatientEditViewModels = new ObservableCollection<IPatientEditViewModel>();
            _patientEditViewModel = patientEditViewModel;
            _searchViewModel = searchViewModel;
            
        }

        public string ClientName { get; set; }

        private WcfProxy Proxy { get; set; }

        public void RegisterClient(string clientName, EventHandler eventHandler)
        {
            BroadcastHandler = eventHandler;            
            
            ProxySetup(clientName, eventHandler, out _wcfproxy);            
        }

        private void OnClosePatientTabExecute(object parameter)
        {
            var patientEditView = parameter as IPatientEditViewModel;
            if(patientEditView != null)
            {
                if (patientEditView.Patient.IsChanged)
                {
                    var result = _messageDialogService.ShowYesNoDialog(typeof(MessageDialog),"Close tab?","You will loose your changes", MessageDialogResult.No);
                    if(result == MessageDialogResult.No)
                    {
                        return;
                    }
                }
                PatientEditViewModels.Remove(patientEditView);
            }
        }       

        public ICommand ClosePatientTabCommand { get; private set; }

        public ObservableCollection<Patient> Patients { get; set; }

        public ObservableCollection<IPatientEditViewModel> PatientEditViewModels { get; private set; }

        public void LoadNavigationView(IEnumerable<Patient> patient)
        {
            _navigationViewModel.Load(patient);
        }

        public void Load()
        {
            var _serviceFactory = ObjectBase.Container.GetExportedValue<IServiceFactory>();
            _serviceProvider = ObjectBase.Container.GetExportedValue<ISvcProvider>();
            _serviceProvider.Context = _wcfproxy.Context;

                IEnumerable<Patient> patients = null;
                WithClient(_serviceFactory.CreateClient<IClinicalDataClient>(_serviceProvider),
                async (broadcastClient) =>
                {
                    broadcastClient.serviceProvider = _serviceProvider;
                    broadcastClient.CreateInstanceContext(_serviceProvider, out _clinicalServiceClient);

                    try
                    {
                        patients = await _clinicalServiceClient.GetAllPatientsAsync();                        
                    }
                    catch(Exception ex)
                    {                        
                        throw ex;
                    }
                    finally
                    {
                        if (patients != null)
                        {
                            LoadNavigationView(patients);

                            Patients.Clear();
                            foreach (var patient in patients)
                            {
                                Patients.Add(patient);
                            }
                            _searchViewModel.Patients = Patients;

                            Dispose();

                            CallUnmanagedPointer(_unmanagedPointer);
                        }
                    }
                });    
        }
        
        public void LoadPatient_Address()
        {
            try
            {
                if (_wcfproxy.Proxy == null)
                {
                    ProxySetup(ClientName, BroadcastHandler, out _wcfproxy);
                }
                var patients = _wcfproxy.Proxy.GetAllPatients_Address();
                LoadNavigationView(patients);

                //ProxyClose(_wcfproxy);

                Patients.Clear();
                foreach (var patient in patients)
                {
                    Patients.Add(patient);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                Dispose();
            }
            CallUnmanagedPointer(_unmanagedPointer);
            // return patients;
        }
        public bool IsChanged => PatientEditViewModels.Any(p => p.Patient.IsChanged );

               
        private void OnOpenPatientTab(Guid patientId)
        {
            try
            {
                IPatientEditViewModel patientEditVm = PatientEditViewModels.SingleOrDefault(vm => vm.Patient.PatientId == patientId);
                if (patientEditVm == null)
                {

                    if (_wcfproxy.Proxy == null)
                    {
                        ProxySetup(ClientName, BroadcastHandler, out _wcfproxy);
                    }
                    patientEditVm = new PatientEditViewModel(_eventAggregator,_patientDepartmentLookupProvider, _doctorLookupProvider,
                        _messageDialogService, _wcfproxy);
                    //_patientEditViewModelCreator(_eventAggregator) ;
                    patientEditVm.Load(patientId);
                    PatientEditViewModels.Add(patientEditVm);

                }
                SelectedPatientEditViewModel = patientEditVm;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                Dispose();
            }

            CallUnmanagedPointer(_unmanagedPointer);
            //ProxyClose(_wcfproxy);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ClientName = navigationContext.Parameters["clientName"] as string;            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {           
        }

        public IPatientEditViewModel SelectedPatientEditViewModel
        {
            get { return _selectedPatientEditViewModel; }
            set
            {
                _selectedPatientEditViewModel = value;
                OnPropertyChanged();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (_wcfproxy.Proxy != null)
                {
                    _wcfproxy.Proxy.Dispose();
                    _wcfproxy.Proxy = null;
                }
                if(_clinicalServiceClient != null)
                {
                    _clinicalServiceClient.Dispose();
                    _clinicalServiceClient = null;
                }

            }
            if (_unmanagedPointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_unmanagedPointer);
                _unmanagedPointer = IntPtr.Zero;
            }
            base.Dispose(disposing);
        }



    }
}
