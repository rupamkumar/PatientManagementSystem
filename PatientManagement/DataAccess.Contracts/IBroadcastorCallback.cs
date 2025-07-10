using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    [ServiceContract]
  public interface IBroadcastorCallback
    {

        [OperationContract(IsOneWay = true)]
        void BroadcastToClient(EventDataType eventData);
    }
}
