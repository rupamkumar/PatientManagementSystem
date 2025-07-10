using PatientManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    [ServiceContract(CallbackContract = typeof(IBroadcastorCallback))]
    public interface IPatientService 
    {
        [OperationContract]
        IEnumerable<Patient> GetAllPatients();

        [OperationContract]
        IEnumerable<Patient> GetAllPatients_Address();

        [OperationContract(Name ="GetAllPatientAsync")]
        Task<IEnumerable<Patient>> GetAllPatientsAsync();

        [OperationContract]
        //[FaultContract(typeof(NotFoundException))]
        Patient GetPatientById(Guid Id);

        [OperationContract]
        Patient GetVisitsByPatientId(Guid PatientId);


        [OperationContract(Name = "SavePatientWithVisitAsync")]
        Task<bool> SavePatientAsync(Patient patient, Visit visit, bool deletedVisit, string deletedVisitId);

        [OperationContract(Name = "SavePatientWithoutVisitAsync")]
        Task<bool> SavePatientAsync(Patient patient, bool deletedVisit, string deletedVisitId);

        [OperationContract]
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();

        [OperationContract]
        Doctor GetDoctorById(Guid Id);


        [OperationContract]
        IEnumerable<Department> GetAllDepartment();

        [OperationContract(Name ="GetAllDepartmentsAsync")]
        Task<IEnumerable<Department>> GetAllDepartmentAsync();

        
    }
       
}
