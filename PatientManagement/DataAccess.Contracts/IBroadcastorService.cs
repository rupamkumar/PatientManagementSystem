using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using RupStoreCore.Common.Contracts;

namespace DataAccess.Contracts
{
    [ServiceContract(CallbackContract = typeof(IBroadcastorCallback))]
   public interface IBroadcastorService
    {
        [OperationContract(IsOneWay = true)]
        void RegisterClient(string clientName);

        [OperationContract(IsOneWay = true)]
        void NotifyServer(EventDataType eventData);
    }
}
