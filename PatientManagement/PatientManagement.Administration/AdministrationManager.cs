using Microsoft.Practices.Prism.Modularity;
using System;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Regions;
using Infrastructure;
using System.ComponentModel.Composition;
using PatientManagement.Administration.Views;


namespace PatientManagement.Administration
{
    [ModuleExport(typeof(AdministrationManager))]
    public class AdministrationManager : IModule
    {
        [Import]
        public IRegionManager RegionManager;
        public void Initialize()
        {
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(AdministrationView));
        }
    }
}
