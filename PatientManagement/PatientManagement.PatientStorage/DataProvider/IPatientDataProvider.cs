using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientManagement.Model;

namespace PatientManagement.PatientStorage.DataProvider
{
  public  interface IPatientDataProvider 
    {

        IEnumerable<Patient> GetAllPatients();

        Patient GetPatientById(Guid patientId);

        Patient GetVisitsByPatientId(Guid patientId);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();

        IEnumerable<Patient> GetAllPatients_Address();

        IEnumerable<Department> GetAllDepartment();

        void SavePatient(Patient patient, Visit visit, bool deletedVisit, string deletedVisitId);

    }
}
