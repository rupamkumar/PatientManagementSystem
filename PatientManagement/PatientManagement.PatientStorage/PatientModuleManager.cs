using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Infrastructure;
using PatientManagement.PatientStorage.Views;
using RupStoreCore.Common.Core;
using DataAccess.Client.Bootstrapper;
using System.ComponentModel.Composition.Primitives;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using DataAccess.Proxies;

namespace PatientManagement.PatientStorage
{
    [ModuleExport(typeof(PatientModuleManager))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PatientModuleManager : IModule
    {

        [Import]
        public IRegionManager RegionManager;
        public void Initialize()
        {
            //ObjectBase.Container = MEFContainer.Init(new List<ComposablePartCatalog>()
            //{
            //    //new AssemblyCatalog(Assembly.GetAssembly(typeof(ClinicalDataClient)))
            //});
            
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(PatientView));
        }
    }
}
