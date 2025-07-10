using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientManagement.Model;

namespace PatientManagement.PatientVault.DataProvider
{
  public  interface IPatientDataProvider 
    {
        IEnumerable<Patient> GetAllPatients();

        Task<IEnumerable<Patient>> GetAllPatientsAsync();

        Patient GetPatientById(Guid? Id);

        IAsyncResult BeginGetPatients(AsyncCallback callback, object userState);

        IEnumerable<Patient> EndGetPatients(IAsyncResult result);


    }
}
