using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.PatientVault.Model
{
   public interface IPatientVaultService
    {
        IAsyncResult BeginGetPatients(AsyncCallback callback, object userState);
        IEnumerable<Patient> EndGetPatients(IAsyncResult result);

        Patient GetPatient(Guid id);
    }
}
