using DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ServiceModel;
using System.ServiceModel.Channels;
using DataAccess.Contracts.ServiceInterface;

namespace DataAccess.Proxies.ServiceProxies
{
    [Export(typeof(IBroadcastorServiceClient))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
   public class BroadcastServiceClient : DuplexClientBase<IBroadcastorService> , IBroadcastorServiceClient
    {
       //[ImportingConstructor]
        public BroadcastServiceClient(InstanceContext instanceContext)
           : base(instanceContext)
        {
        }
        public BroadcastServiceClient(InstanceContext instanceContext, string endpointName)
            : base(instanceContext, endpointName)
        {
        }

        public BroadcastServiceClient(InstanceContext instanceContext, Binding binding, EndpointAddress address)
            : base(instanceContext, binding, address)
        {
        }
        public void NotifyServer(EventDataType eventData)
        {
            Channel.NotifyServer(eventData);
        }

        public void RegisterClient(string clientName)
        {
            Channel.RegisterClient(clientName);
        }
    }
}
