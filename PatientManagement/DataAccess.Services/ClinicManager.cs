
using DataAccess.Contracts;
using DataAccess.Repository;
using DataAccess.ServiceInterface;
using PatientManagement.Model;
using RupStoreCore.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    [ServiceBehavior( ConcurrencyMode = ConcurrencyMode.Multiple, ReleaseServiceInstanceOnTransactionComplete = false)]
    public class ClinicManager : IPatientService, IBroadcastorService
    {
        IPatientRepository _patientRepository;
        IDoctorRepository _doctorRepository;
        private DatabaseSettings da = DatabaseSettings.Instance;

        //private static string _dataModel = ConfigurationSettings.AppSettings["DataConnection"];
        //string _dataModel = "PmsModel";

        public ClinicManager()
        {
        }

        public ClinicManager(IPatientRepository patientRepository, IDoctorRepository doctorRepository, string DataModel)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            //_dataModel = DataModel;
        }
               
        public IEnumerable<Department> GetAllDepartment()
        {
            IPatientRepository patientRepository = _patientRepository ?? new PatientRepository(da.DataConnection);
            return patientRepository.GetAllDepartment();
        }

        public Task<IEnumerable<Department>> GetAllDepartmentAsync()
        {
            IPatientRepository patientRepository = _patientRepository ?? new PatientRepository(da.DataConnection);
            return patientRepository.GetAllDepartmentAsync();
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            IPatientRepository patientRepository = _patientRepository ?? new PatientRepository(da.DataConnection);
            var results=  patientRepository.GetAllPatients();
            
            return results;
        }

        public Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            
            IPatientRepository patientRepository = _patientRepository ?? new PatientRepository(da.DataConnection);
            var allPatients =  patientRepository.GetAllPatientsAsync();
           
            return allPatients;
        }

        public IEnumerable<Patient> GetAllPatients_Address()
        {
            IPatientRepository patientRepository = _patientRepository ?? new PatientRepository(da.DataConnection);
            return patientRepository.GetAllPatients_Address();
        }

        public Patient GetPatientById(Guid Id)
        {
            IPatientRepository patientRepository = _patientRepository ?? new PatientRepository(da.DataConnection);
            return patientRepository.GetPatientById(Id);
        }

        public Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            IDoctorRepository doctorRepository = _doctorRepository ?? new DoctorRepository(da.DataConnection);
            return doctorRepository.GetAllDoctorsAsync();
        }

        public Doctor GetDoctorById(Guid Id)
        {
            IDoctorRepository doctorRepository = _doctorRepository ?? new DoctorRepository(da.DataConnection);
            return doctorRepository.GetDoctorById(Id);
        }

        public Patient GetVisitsByPatientId(Guid PatientId)
        {
            IPatientRepository patientRepository = _patientRepository ?? new PatientRepository(da.DataConnection);
            return patientRepository.GetVisitsByPatientId(PatientId);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public  Task<bool> SavePatientAsync(Patient patient, Visit visit, bool deletedVisit, string deletedVisitId)
        {
            IPatientRepository patientRepository = _patientRepository ?? new PatientRepository(da.DataConnection);
            var result =  patientRepository.SavePatientAsync(patient, visit, deletedVisit, deletedVisitId);
            IBroadcastorCallback callback = OperationContext.Current.GetCallbackChannel<IBroadcastorCallback>();
            if(callback != null)
            {
               // MessageBox.Show("Called the client, back at service.");
            }
            return result;
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public Task<bool> SavePatientAsync(Patient patient, bool deletedVisit, string deletedVisitId)
        {
            IPatientRepository patientRepository = _patientRepository ?? new PatientRepository(da.DataConnection);
            var result =   patientRepository.SavePatientAsync(patient, deletedVisit, deletedVisitId);
            IBroadcastorCallback callback = OperationContext.Current.GetCallbackChannel<IBroadcastorCallback>();
            if (callback != null)
            {
                //MessageBox.Show("Called the client, back at service.");
            }
            return result;
        }


        private static Dictionary<string, IBroadcastorCallback> clients = new Dictionary<string, IBroadcastorCallback>();
        private static object locker = new object();
        public void RegisterClient(string clientName)
        {
            if (clientName != null && clientName != "")
            {
                try
                {
                    IBroadcastorCallback callback =
                        OperationContext.Current.GetCallbackChannel<IBroadcastorCallback>();
                    lock (locker)
                    {
                        //remove the old client
                        if (clients.Keys.Contains(clientName))
                            clients.Remove(clientName);
                        clients.Add(clientName, callback);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        public void NotifyServer(EventDataType eventData)
        {
            lock (locker)
            {
                var inactiveClients = new List<string>();
                foreach (var client in clients)
                {
                    if (client.Key != eventData.ClientName)
                    {
                        try
                        {
                            client.Value.BroadcastToClient(eventData);
                        }
                        catch (Exception ex)
                        {
                            inactiveClients.Add(client.Key);
                        }
                        
                    }
                }

                if (inactiveClients.Count > 0)
                {
                    foreach (var client in inactiveClients)
                    {
                        clients.Remove(client);
                    }
                }
            }
        }

        
    }
}
