using DataAccess.Contracts.ServiceInterface;
using RupStoreCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Proxies.ServiceProxies
{
    [Export(typeof(IClinicalDataClient))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class ClinicalDataClient : IClinicalDataClient
    {
        ISvcProvider _serviceProvider;

        [ImportingConstructor]
        public ClinicalDataClient(ISvcProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var context = _serviceProvider.Context;
        }
        public ISvcProvider serviceProvider { get ; set ; }

        public void CreateInstanceContext(ISvcProvider serviceProvider, out IClinicalDataServiceClient serviceproxy)
        {
            serviceproxy = new ClinicalDataServiceClient(serviceProvider.Context);
        }
    }
}
