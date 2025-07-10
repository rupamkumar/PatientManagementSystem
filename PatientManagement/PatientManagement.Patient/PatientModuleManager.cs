using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Regions;
using Infrastructure;
using PatientManagement.Patient.Views;

namespace PatientManagement.Patient
{
    [ModuleExport(typeof(PatientModuleManager))]
    public class PatientModuleManager : IModule
    {

        [Import]
        public IRegionManager RegionManager;
        public void Initialize()
        {
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(PatientView));
        }
    }
}
