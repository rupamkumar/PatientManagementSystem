using DataAccess.Contracts.ServiceInterface;
using Infrastructure.DataProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.PatientStorage.DataProvider
{
    [Export(typeof(IWcfProxy))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class WcfProxyProvider : IWcfProxy
    {
        public string ClientName { get ; set ; }
        public InstanceContext Context { get ; set ; }
        public EventHandler BroadCastHandler { get; set; }
        public IClinicalDataServiceClient Proxy { get ; set; }

        public IBroadcastorServiceClient BroadcastorProxy { get; set; }

        [ImportingConstructor]
        public WcfProxyProvider()
        {

        }

        public void Dispose()
        {
            if (this != null)
            {
                this.Dispose();
            }

        }
    }
}
