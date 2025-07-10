using System.ComponentModel.Composition;
using DataAccess.Contracts.ServiceInterface;
using RupStoreCore.Common.Contracts;
using System;

namespace DataAccess.Proxies.ServiceProxies
{
    [Export(typeof(IBroadcastClient))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BroadcastClient : IBroadcastClient
    {
        
        ISvcProvider _serviceProvider;

        [ImportingConstructor]
        public BroadcastClient(ISvcProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var context = _serviceProvider.Context;
        }

        public ISvcProvider serviceProvider { get ; set ; }

        public void CreateInstanceContext(ISvcProvider serviceProvider, out IBroadcastorServiceClient broadcastproxy)
        {
            broadcastproxy = new BroadcastServiceClient(serviceProvider.Context);
            broadcastproxy.RegisterClient(serviceProvider.ClientName);
        }
                
    }
}
