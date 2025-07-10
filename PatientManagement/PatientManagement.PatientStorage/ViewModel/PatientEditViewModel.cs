using Infrastructure.Base;
using PatientManagement.Model;
using PatientManagement.PatientStorage.DataProvider;
using PatientManagement.PatientStorage.Wrapper;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using PatientManagement.PatientStorage.Events;
using Infrastructure.ServiceInterface;
using System.Collections.ObjectModel;
using Infrastructure.Base.Events;
using Infrastructure.DataProvider;
using Infrastructure;
using System.Windows.Data;
using Infrastructure.Behaviors;

using System.Xml.Linq;
using DataAccess.Proxies;
using Microsoft.Practices.Prism.Regions;
using DataAccess.Contracts;
using System.Threading;
using System.ServiceModel;
using RupStoreCore.Common.Exceptions;
using System.Runtime.InteropServices;
using RupStoreCore.Common.Core;
using RupStoreCore.Common.Contracts;
using DataAccess.Contracts.ServiceInterface;

namespace PatientManagement.PatientStorage.ViewModel
{

    public interface IPatientEditViewModel 
    {
        void Load(Guid? patientId = null);

        PatientWrapper Patient { get; }

    }

    [Export(typeof(IPatientEditViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public  class PatientEditViewModel : ViewModelBase, IPatientEditViewModel
    {
        private PatientWrapper _patient;
        private VisitWrapper _selectedVisit;
        private IEnumerable<LookupItem> _patientDepartment;
        private IEnumerable<LookupItem> _doctor;
       
        private readonly ILookupProvider<Department> _patientDepartmentLookupProvider;
        private readonly ILookupProvider<Doctor> _doctorLookupProvider;
        private readonly IMessageDialogService _messageDialogService;

        private readonly IEventAggregator _eventAggregator;

        private  IWcfProxy _wcfproxy;
        bool disposed = false;
        private IntPtr _unmanagedPointer;

        IClinicalDataServiceClient _clinicalServiceClient;
        IBroadcastorServiceClient _broadcastServiceClient;
        

        private ISvcProvider _serviceProvider;

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;



        [ImportingConstructor]
        public PatientEditViewModel(IEventAggregator eventAggregator, 
            ILookupProvider<Department> patientDepartmentLookupProvider, ILookupProvider<Doctor> doctorLookupProvider, IMessageDialogService messageDialogService,
            IWcfProxy wcfproxy)
        {
            
            _eventAggregator = eventAggregator;
            _patientDepartmentLookupProvider = patientDepartmentLookupProvider;
            _doctorLookupProvider = doctorLookupProvider;
            _messageDialogService = messageDialogService;
            _wcfproxy = wcfproxy;
            SetupCommand();
        }

        private void SetupCommand()
        {
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            ResetCommand = new DelegateCommand(OnResetExecute, OnResetCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute, OnDeleteCanExecute);

            AddVisitCommand = new DelegateCommand(OnAddVisitExecute);
            DeleteVisitCommand = new DelegateCommand(OnDeleteVisitExecute, OnDeleteVisitCanExecute);
        }

        private void OnAddVisitExecute()
        {
            Patient.Visits.Add(new VisitWrapper(new Visit()));                       
            
        }

        private bool OnDeleteVisitCanExecute()
        {
            return SelectedVisit != null;
        }

        private void OnDeleteVisitExecute()
        {
            Patient.DeletedVisit.Add(SelectedVisit);

            Patient.Visits.Remove(SelectedVisit);
            
            ((DelegateCommand)DeleteVisitCommand).RaiseCanExecuteChanged();
        }

        private bool OnDeleteCanExecute()
        {
            return Patient != null && Patient.PatientId != Guid.Empty;
        }

        private void OnDeleteExecute()
        {
            //var winform = new MessageDialogServices<MainWindow>();
            var result = _messageDialogService.ShowYesNoDialog(typeof(MessageDialog),"Delete Patient?",
                String.Format("Are you sure, you want to Delete Patient '{0} {1}'?", Patient.FirstName, Patient.LastName),
                MessageDialogResult.No);
            if (result == MessageDialogResult.Yes)
            {

            }            
        }

        private bool OnResetCanExecute()
        {
            return Patient.IsChanged; ;
        }

        private void OnResetExecute()
        {
            Patient.RejectChanges();
        }
        public PatientWrapper Patient
        {
            get { return _patient; }
            private set
            {
                _patient = value;
                OnPropertyChanged();
            }
        }
        public VisitWrapper SelectedVisit
        {
            get { return _selectedVisit; }
            set
            {
                _selectedVisit = value;
                OnPropertyChanged();
                ((DelegateCommand)DeleteVisitCommand).RaiseCanExecuteChanged();
            }
        }
        private void InvalidateCommands()
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)ResetCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
        }       

        public ICommand SaveCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddVisitCommand { get; private set; }
        public ICommand DeleteVisitCommand { get; private set; }

        private bool OnSaveCanExecute()
        {           
            return Patient.IsChanged;
        }

        private void OnSaveExecute()
        {
         
            var updatefName = Patient.FirstNameOriginalValue;
            var updatedLName = Patient.LastNameOriginalValue;
            if (Patient.IsValid)
            {
                var deletedVisit = Patient.DeletedVisit;
                List<Guid> visitId = new List<Guid>();
                XDocument doc = new XDocument();
                XElement elem = new XElement("Root");

                try
                {
                    if (deletedVisit.Count > 0)
                    {
                        //for the time being only one data; have to pass multiple data xml format to store procedure.
                        deletedVisit.ToList().ForEach(d => visitId.Add(d.VisitId));

                        foreach (var id in visitId)
                        {
                            var vid = new XElement("visitId", id);
                            var visit = new XElement("Visit", vid);
                            elem.Add(new XElement(visit));
                        }
                        doc.Add(elem);
                    }
                }
                //catch (FaultException ex)
                //{
                //    if (ErrorOccured != null)
                //    {
                //        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                //    }
                //}
                catch (Exception ex)
                {
                    if (ErrorOccured != null)
                    {
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                    }
                }

                ProxySetup(_wcfproxy.ClientName, _wcfproxy.BroadCastHandler, out _wcfproxy);

                var _serviceFactory = ObjectBase.Container.GetExportedValue<IServiceFactory>();
                _serviceProvider = ObjectBase.Container.GetExportedValue<ISvcProvider>();
                _serviceProvider.Context = _wcfproxy.Context;
                _serviceProvider.ClientName = _wcfproxy.ClientName;
                _serviceProvider.BroadCastHandler = _wcfproxy.BroadCastHandler;


                WithClient(_serviceFactory.CreateClient<IClinicalDataClient>(_serviceProvider),
                 (clinicalDataClient) =>
                 {
                     clinicalDataClient.serviceProvider = _serviceProvider;
                     clinicalDataClient.CreateInstanceContext(_serviceProvider, out _clinicalServiceClient);
                     Task.Run(() =>
                                {
                                    try
                                    {
                                        Task<bool> task;
                                        if (SelectedVisit != null)
                                        {
                                            //_proxy.SavePatient(Patient.Wrapper, SelectedVisit.Wrapper, (deletedVisit.Count > 0), doc.ToString());
                                            task = _clinicalServiceClient.SavePatientAsync(Patient.Wrapper, SelectedVisit.Wrapper, (deletedVisit.Count > 0), doc.ToString());
                                        }
                                        else
                                        {
                                            // _proxy.SavePatient(Patient.Wrapper, null, (deletedVisit.Count > 0), doc.ToString());
                                            task = _clinicalServiceClient.SavePatientAsync(Patient.Wrapper, (deletedVisit.Count > 0), doc.ToString());
                                        }
                                    }
                                    catch(Exception ex)
                                    {
                                        throw ex;
                                    }
                                    
                                });
                 });

                WithClient(_serviceFactory.CreateClient<IBroadcastClient>(_serviceProvider),
                 (broadcastClient) =>
                 {
                     try
                     {
                         broadcastClient.serviceProvider = _serviceProvider;
                         broadcastClient.CreateInstanceContext(_serviceProvider, out _broadcastServiceClient);
                         Task.Run(() =>
                         {
                             try
                             {
                                 _broadcastServiceClient.NotifyServer(
                                new EventDataType()
                                {
                                    ClientName = _wcfproxy.ClientName,
                                    EventMessage = string.Format("Patient {0} {1} have been updated", updatefName, updatedLName)
                                });
                             }
                             catch(Exception ex)
                             {
                                 throw ex;
                             }
                         });
                         Task.Run(() =>
                         {
                             try
                             {
                                 Thread.Sleep(10000);
                                 _broadcastServiceClient.NotifyServer(
                                   new EventDataType()
                                   {
                                       ClientName = _wcfproxy.ClientName,
                                       EventMessage = string.Empty
                                   });
                             }
                             catch(Exception ex)
                             {
                                 throw ex;
                             }
                             finally
                             {
                                 Dispose();
                             }
                         });

                         Patient.AcceptChanges();
                         _eventAggregator.GetEvent<WcfEvent<Patient>>().Publish(Patient.Wrapper);

                         InvalidateCommands();
                     }
                     catch (FaultException ex)
                     {
                         if (ErrorOccured != null)
                         {
                             ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                         }
                     }
                     catch (Exception ex)
                     {
                         if (ErrorOccured != null)
                         {
                             ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                         }
                     }
                     finally
                     {
                         
                     }
                 });
                                         
                
                Patient.AcceptChanges();
                _eventAggregator.GetEvent<WcfEvent<Patient>>().Publish(Patient.Wrapper);

                InvalidateCommands();
                

                // CallUnmanagedPointer(_unmanagedPointer);
            }
            //_eventAggregator.GetEvent<SearchEventBase<Patient>>().Publish(Patient.Wrapper);
            
        }

        
        public IEnumerable<LookupItem> PatientDepartmentLookup
        {
            get { return _patientDepartment; }
            set
            {
                _patientDepartment = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<LookupItem> DoctorLookup
        {
            get { return _doctor; }
            set
            {
                _doctor = value;
                OnPropertyChanged();
            }
        }

        public void Load(Guid? patientId = null)
        {
            
            ProxySetup(_wcfproxy.ClientName, _wcfproxy.BroadCastHandler, out _wcfproxy);
            var _serviceFactory = ObjectBase.Container.GetExportedValue<IServiceFactory>();
            _serviceProvider = ObjectBase.Container.GetExportedValue<ISvcProvider>();
            _serviceProvider.Context = _wcfproxy.Context;

            
            WithClient(_serviceFactory.CreateClient<IClinicalDataClient>(_serviceProvider),
             (clinicalDataClient) =>
            {
                clinicalDataClient.serviceProvider = _serviceProvider;
                clinicalDataClient.CreateInstanceContext(_serviceProvider, out _clinicalServiceClient);
                
                try
                {
                    Task<IEnumerable<Department>> task = _clinicalServiceClient.GetAllDepartmentAsync();
                    PatientDepartmentLookup = _patientDepartmentLookupProvider.GetLookup(task.Result.AsEnumerable<Department>());
                    Task<IEnumerable<Doctor>> doctors = _clinicalServiceClient.GetAllDoctorsAsync();
                    DoctorLookup = _doctorLookupProvider.GetLookup(doctors.Result.AsEnumerable<Doctor>());
                }
                catch (FaultException ex)
                {
                    if (ErrorOccured != null)
                    {
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                    }
                }
                catch (Exception ex)
                {
                    if (ErrorOccured != null)
                    {
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                    }
                }

                try
                {
                    var patient = patientId.HasValue
                   ? _clinicalServiceClient.GetPatientById(patientId.Value)
                   : new Patient { Address = new Address(), Visits = new List<Visit>() };
                    Patient = new PatientWrapper(patient);

                    Patient.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == nameof(Patient.IsChanged)
                            || e.PropertyName == nameof(Patient.IsValid))
                        {
                            InvalidateCommands();
                        }
                    };
                }
                catch (FaultException ex)
                {
                    if (ErrorOccured != null)
                    {
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                    }
                }
                catch (Exception ex)
                {
                    if (ErrorOccured != null)
                    {
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                    }
                }
                finally
                {
                    Dispose();
                }

                InvalidateCommands();

                CallUnmanagedPointer(_unmanagedPointer);
            });


            //try
            //{
            //    Task<IEnumerable<Department>> task = _wcfproxy.Proxy.GetAllDepartmentAsync();

            //    PatientDepartmentLookup = _patientDepartmentLookupProvider.GetLookup(task.Result.AsEnumerable<Department>());
            //    Task<IEnumerable<Doctor>> doctors = _wcfproxy.Proxy.GetAllDoctorsAsync();
            //    DoctorLookup = _doctorLookupProvider.GetLookup(doctors.Result.AsEnumerable<Doctor>());
            //}           


            //catch (FaultException ex)
            //{
            //    if (ErrorOccured != null)
            //    {
            //        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (ErrorOccured != null)
            //    {
            //        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
            //    }
            //}           
           


            //try
            //{
            //    var patient = patientId.HasValue
            //   ? _wcfproxy.Proxy.GetPatientById(patientId.Value)
            //   : new Patient { Address = new Address(), Visits = new List<Visit>() };
            //    Patient = new PatientWrapper(patient);

            //    Patient.PropertyChanged += (s, e) =>
            //    {
            //        if (e.PropertyName == nameof(Patient.IsChanged)
            //            || e.PropertyName == nameof(Patient.IsValid))
            //        {
            //            InvalidateCommands();
            //        }

            //    };
            //}
            //catch (FaultException ex)
            //{
            //    if (ErrorOccured != null)
            //    {
            //        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (ErrorOccured != null)
            //    {
            //        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
            //    }
            //}
            //finally
            //{
            //    Dispose();
            //}
            
            //InvalidateCommands();            

            //CallUnmanagedPointer(_unmanagedPointer);
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if(_wcfproxy.Proxy != null)
                {
                    _wcfproxy.Proxy.Dispose();
                    _wcfproxy.Proxy = null;
                }
                //if(_broadcastServiceClient != null)
                //{
                //    _broadcastServiceClient.Dispose();
                //    _broadcastServiceClient = null;
                //}
                if(_clinicalServiceClient != null)
                {
                    _clinicalServiceClient.Dispose();
                    _clinicalServiceClient = null;
                }
            }
            if(_unmanagedPointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_unmanagedPointer);
                _unmanagedPointer = IntPtr.Zero;
            }
            base.Dispose(disposing);
        }

    }
}
