using Infrastructure;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using PatientManagement.PatientVault.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.PatientVault
{
    [ModuleExport(typeof(PatientsVaultModule))]
    public class PatientsVaultModule : IModule
    {
        [Import]
        public IRegionManager RegionManager;
       
        public void Initialize()
        {
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(MainView));
            //this.RegionManager.RegisterViewWithRegion(RegionNames.SubContentRegion, typeof(MainView));
        }
    }
}
