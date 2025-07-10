using DataAccess.Contracts.ServiceInterface;
using Infrastructure.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Base
{
   public class WcfProxy : IWcfProxy
    {

        public string ClientName { get; set; }
        public System.ServiceModel.InstanceContext Context { get; set; }
        public EventHandler BroadCastHandler { get; set; }
        public IClinicalDataServiceClient Proxy { get; set; }
        public IBroadcastorServiceClient BroadcastorProxy { get; set; }
        
    }
}
