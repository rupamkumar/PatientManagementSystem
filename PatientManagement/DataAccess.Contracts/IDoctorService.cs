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
    public  interface IDoctorService
    {

        [OperationContract]
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();

        [OperationContract]
        Doctor GetDoctorById(Guid Id);
    }
}
