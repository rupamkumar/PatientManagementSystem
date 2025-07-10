using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RupStoreCore.Common.Contracts
{
   public interface ISvcProvider
    {
        string ClientName { get; set; }
        InstanceContext Context { get; set; }
        EventHandler BroadCastHandler { get; set; }
    }
}
