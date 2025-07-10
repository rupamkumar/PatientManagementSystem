using DataAccess.Contracts;
using System;
using RupStoreCore.Common.Contracts;
using System.ServiceModel;

namespace DataAccess.Contracts.ServiceInterface
{
   // [ServiceContract]
  public  interface IBroadcastorServiceClient :IBroadcastorService, IDisposable, IServiceContract
    {
        void Close();
        void Open();
    }
}
