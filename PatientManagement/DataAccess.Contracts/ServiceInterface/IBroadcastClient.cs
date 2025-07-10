using RupStoreCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts.ServiceInterface
{
  public  interface IBroadcastClient : IServiceContract
    {
        void CreateInstanceContext(ISvcProvider serviceProvider, out IBroadcastorServiceClient serviceproxy);
        ISvcProvider serviceProvider { get; set; }
    }
}
