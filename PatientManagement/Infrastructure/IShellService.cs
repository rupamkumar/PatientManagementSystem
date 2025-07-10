using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
   public interface IShellService 
    {        
        void ShowShell(string url);
        void ShowShell(IRegionManager _regionManager, string url);
    }
}
