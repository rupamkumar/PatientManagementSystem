using Infrastructure;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PatientManagement.TMS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.TMS
{
    [ModuleExport(typeof(TMSManager))]
    public class TMSManager : IModule
    {
        //IRegionManager _regionManager;
        //IUnityContainer _container;

        //public ModuleVaultManager(IRegionManager regionManager, IUnityContainer container)
        //{
        //    _regionManager = regionManager;
        //    _container = container;
        //}
        //public void Initialize()
        //{
        //    _container.RegisterType(typeof(object), typeof(ViewB), "ViewB");
        //}

            
        [Import]
        public IRegionManager RegionManager;

        public void Initialize()
        {
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(ViewB));
        }
    }
}
