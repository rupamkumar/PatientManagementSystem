using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository;
using DataAccess.ServiceInterface;
using PatientManagement.Model;

namespace PatientManagement.PatientStorage.DataProvider
{
    [Export(typeof(IDoctorDataProvider))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DoctorDataProvider : IDoctorDataProvider
    {
        private IDoctorRepository _repo;

        [ImportingConstructor]
        public DoctorDataProvider()
             : this(new DoctorRepository("PmsModel"))
        {

        }
        public DoctorDataProvider(IDoctorRepository repo)
        {
            _repo = repo;
        }

     

        public Task<IEnumerable<Doctor>> GetAllDoctorAsync()
        {
            return _repo.GetAllDoctorsAsync();
        }

        public Doctor GetDoctorById(Guid doctorId)
        {
            return _repo.GetDoctorById(doctorId);
        }
        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _repo.GetAllDoctors();
        }
    }
}
