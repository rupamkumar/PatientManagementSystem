using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository;
using DataAccess.ServiceInterface;
using PatientManagement.Model;
using DataAccess.Proxies;

namespace PatientManagement.PatientStorage.DataProvider
{
    [Export(typeof(IPatientDataProvider))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PatientDataProvider : IPatientDataProvider
    {
        //private readonly Func<IPatientRepository> _dataserviceCreator;
       
        private IPatientRepository _repo;
        //private IClinicalDataServiceClient _repo;
        private static string DataConnection = ConfigurationSettings.AppSettings["DataConnection"]; 
        private static string context = ConfigurationSettings.AppSettings["Binding"];

        [ImportingConstructor]
        public PatientDataProvider()
            : this(new PatientRepository(DataConnection))
        {
        }

        //[ImportingConstructor]
        //public PatientDataProvider()
        //    : this(new ClinicalDataClient(Inscontext))
        //{
        //}

        //[ImportingConstructor]
        //public PatientDataProvider(IClinicalDataServiceClient repo)
        ////:this(Activator.CreateInstance<PatientRepository>)
        //{
        //    _repo = repo;
        //    //repo.Close();
        //}

        public PatientDataProvider(IPatientRepository repo)
        //:this(Activator.CreateInstance<PatientRepository>)
        {
            _repo = repo;
        }

        public Patient Add(Patient patient)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Department> GetAllDepartment()
        {            
            return _repo.GetAllDepartment();
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _repo.GetAllPatients();
        }

        
        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _repo.GetAllPatientsAsync();
        }

        public IEnumerable<Patient> GetAllPatients_Address()
        {
            return _repo.GetAllPatients_Address();
        }

        public Patient GetPatientById(Guid patientId)
        {
            return _repo.GetPatientById(patientId);
        }

        public Patient GetVisitsByPatientId(Guid patientId)
        {         
            return _repo.GetVisitsByPatientId(patientId);
        }

        public void RemovePatient(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void SavePatient(Patient patient, Visit visit, bool deletedVisit, string deletedVisitId)
        {
            //if(visit == null)
            //{
            //    _repo.SavePatient(patient, deletedVisit, deletedVisitId);
            //}
            //else
            //{
            //    _repo.SavePatient(patient, visit, deletedVisit, deletedVisitId);
            //}
           
        }

       

        public Patient Update(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
