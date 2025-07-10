using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using System.ComponentModel.Composition.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Regions;
using Infrastructure.Prism;
using Microsoft.Practices.Prism.Mvvm;
using System.Reflection;
using System.Globalization;
using Infrastructure;

namespace PatientManagementUI
{
    public class Bootstrapper : MefBootstrapper
    {
        //private const string ModuleCatalogUri = "/PatientManagementUI;component/ModulesCatalog.xaml";

        public Bootstrapper()
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);
                return Type.GetType(viewModelName);
            });
        }
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            
        }
        protected override IModuleCatalog CreateModuleCatalog()
        {           
            return new ConfigurationModuleCatalog();
        }
        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    ModuleCatalog catalog = new ModuleCatalog();
        //    catalog.AddModule(typeof(PatientManagement.PatientVault.PatientsVaultModule));
        //    return catalog;
        //}
        protected override DependencyObject CreateShell()
        {            
            return this.Container.GetExportedValue<MainWindow>();
            //return this.Container.Resolve<MainWindow>();
        }

        //protected override void ConfigureModuleCatalog()
        //{
        //    ModuleCatalog catalog = (ModuleCatalog)ModuleCatalog;
        //    catalog.AddModule(typeof(ModuleVault.ModuleVaultManager));
        //}
        protected override void InitializeShell()
        {
            base.InitializeShell();
            var regionManager = RegionManager.GetRegionManager(Shell);
            RegionManagerAware.SetRegionManagerAware(Shell, regionManager);
           // Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
