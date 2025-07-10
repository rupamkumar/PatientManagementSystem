using RupStoreCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts.ServiceInterface
{
   public interface IClinicalDataClient : IServiceContract
    {
        void CreateInstanceContext(ISvcProvider serviceProvider, out IClinicalDataServiceClient serviceproxy);
        ISvcProvider serviceProvider { get; set; }
    }
}
