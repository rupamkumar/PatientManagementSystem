using System;
using System.ServiceModel;
using RupStoreCore.Common.Contracts;

namespace DataAccess.Contracts
{
   public interface IServiceFactory
    {
        T CreateClient<T>() where T : IServiceContract;
        T CreateClient<T>(ISvcProvider serviceProvider) where T : IServiceContract;
    }
}
