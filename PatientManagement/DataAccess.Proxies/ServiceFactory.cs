
using System.ComponentModel.Composition;
using DataAccess.Contracts;
using DataAccess.Contracts.ServiceInterface;
using RupStoreCore.Common.Core;
using RupStoreCore.Common.Contracts;

namespace DataAccess.Proxies
{
    [Export(typeof(IServiceFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ServiceFactory : IServiceFactory
    {
        //IBroadcastClient _broadcast;
        T IServiceFactory.CreateClient<T>(ISvcProvider  serviceProvider) 
        {            
            return ObjectBase.Container.GetExportedValue<T>();
        }

        T IServiceFactory.CreateClient<T>()
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}
