﻿using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Prism
{
  public  interface IRegionManagerAware
    {
        IRegionManager RegionManager { get; set; }
    }
}
