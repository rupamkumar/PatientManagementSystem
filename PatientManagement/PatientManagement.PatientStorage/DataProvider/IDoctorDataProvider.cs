using PatientManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.PatientStorage.DataProvider
{
   public interface IDoctorDataProvider
    {

        Doctor GetDoctorById(Guid doctorId);

        Task<IEnumerable<Doctor>> GetAllDoctorAsync();

        IEnumerable<Doctor> GetAllDoctors();
    }
}
