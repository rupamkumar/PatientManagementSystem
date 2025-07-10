using DataAccess.Contracts;
using DataAccess.Repository;
using DataAccess.ServiceInterface;
using PatientManagement.Model;
using RupStoreCore.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, ReleaseServiceInstanceOnTransactionComplete = false)]
    public class DoctorService : IDoctorService, IBroadcastorService
    {

        IDoctorRepository _doctorRepository;

        private  DatabaseSettings da = DatabaseSettings.Instance; 
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

        public void NotifyServer(EventDataType eventData)
        {
            throw new NotImplementedException();
        }

        public void RegisterClient(string clientName)
        {
            throw new NotImplementedException();
        }
    }
}
