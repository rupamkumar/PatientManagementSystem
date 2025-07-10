using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RupStoreCore.Common.Contracts
{
  public  interface IDatabaseSettings
    {
        string DataConnection { get; set; }
    }
}
