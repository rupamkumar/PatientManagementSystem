using Microsoft.Practices.Prism.Modularity;
using System;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Regions;
using Infrastructure;
using System.ComponentModel.Composition;
using PatientManagement.CommandCentre.Views;

namespace PatientManagement.CommandCentre
{
    [ModuleExport(typeof(CommandCentreManager))]
    public class CommandCentreManager : IModule
    {
      

        [Import]
        public IRegionManager RegionManager;

        public void Initialize()
        {
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(CommandCentreView));
        }
    }
}
