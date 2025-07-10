using System;
using System.ComponentModel.Composition;
using System.ServiceModel;
using DataAccess.Contracts;
using RupStoreCore.Common.Contracts;

namespace DataAccess.Proxies
{
    [Export(typeof(ISvcProvider))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ServiceProvider : ISvcProvider
    {
       
        public ServiceProvider()
        {
            
        }
        
        public InstanceContext Context { get ; set ; }
        public string ClientName { get ; set ; }
        public EventHandler BroadCastHandler { get ; set ; }
    }
}
