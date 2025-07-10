using DataAccess.Contracts.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataProvider
{
  public  interface IWcfProxy 
    {
        string ClientName { get; set; }
        System.ServiceModel.InstanceContext Context { get; set; }
        EventHandler BroadCastHandler { get; set; }
        IClinicalDataServiceClient Proxy { get; set; }
        IBroadcastorServiceClient BroadcastorProxy { get; set; }
    }
}
