using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataAccess.Contracts.ServiceInterface;
using PatientManagement.Model;
using System.ComponentModel.Composition;

namespace DataAccess.Proxies.ServiceProxies
{
    [Export(typeof(IClinicalDataServiceClient))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ClinicalDataServiceClient : DuplexClientBase<IPatientService>, IClinicalDataServiceClient
    {
        public ClinicalDataServiceClient(InstanceContext instanceContext)
            :base(instanceContext)
        {
        }
        public ClinicalDataServiceClient(InstanceContext instanceContext,string endpointName)
            :base(instanceContext,endpointName)
        {
        }

        public ClinicalDataServiceClient(InstanceContext instanceContext, Binding binding, EndpointAddress address)
            :base(instanceContext,binding, address)
        {
        }

        public Patient Add(Patient patient)
        {            
            throw new NotImplementedException();
        }

        public IEnumerable<Department> GetAllDepartment()
        {
            return Channel.GetAllDepartment();
        }

        public Task<IEnumerable<Department>> GetAllDepartmentAsync()
        {
            return Channel.GetAllDepartmentAsync();
        }

        public Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return Channel.GetAllDoctorsAsync();
        }
        public Doctor GetDoctorById(Guid Id)
        {
            return Channel.GetDoctorById(Id);
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return Channel.GetAllPatients();
        }

        public Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return Channel.GetAllPatientsAsync();
        }

        public IEnumerable<Patient> GetAllPatients_Address()
        {
            return Channel.GetAllPatients_Address();
        }

        public Patient GetPatientById(Guid Id)
        {
            return Channel.GetPatientById(Id);
        }

        public Patient GetVisitsByPatientId(Guid PatientId)
        {
            return Channel.GetVisitsByPatientId(PatientId);
        }

        

        public void RemovePatient(Guid Id)
        {
            throw new NotImplementedException();
        }

        public bool SavePatient(Patient patient, Visit visit, bool deletedVisit, string deletedVisitId)
        {
            return false;
        }

        public bool SavePatient(Patient patient, bool deletedVisit, string deletedVisitId)
        {
            return false;
        }

        public  Task<bool> SavePatientAsync(Patient patient, Visit visit, bool deletedVisit, string deletedVisitId)
        {
           return  Channel.SavePatientAsync(patient, visit, deletedVisit, deletedVisitId);
        }

        public Task<bool> SavePatientAsync(Patient patient, bool deletedVisit, string deletedVisitId)
        {
            return Channel.SavePatientAsync(patient, deletedVisit, deletedVisitId);
        }
              
    }
}
