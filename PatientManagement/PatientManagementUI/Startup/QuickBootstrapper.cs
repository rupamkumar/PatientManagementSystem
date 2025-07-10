using Infrastructure;
using Infrastructure.Prism;
using PatientManagement.PatientVault;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PatientManagement.TMS;

namespace PatientManagementUI.Startup
{
    public class QuickBootstrapper : UnityBootstrapper
    {
        public QuickBootstrapper()
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);
                return Type.GetType(viewModelName);
            });
        }

        

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();            
        }
        protected override void InitializeShell()
        {
            base.InitializeShell();
            var regionManager = RegionManager.GetRegionManager(Shell);
            RegionManagerAware.SetRegionManagerAware(Shell, regionManager);
            App.Current.MainWindow.Show();
        }
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            ViewModelLocationProvider.SetDefaultViewModelFactory((type) =>
            {
                return Container.Resolve(type);
            });
            this.Container.RegisterType<IShellService, MainWindowService>(new ContainerControlledLifetimeManager());
            //this.Container.RegisterType<IMainWindowService, MainWindowService>(new ContainerControlledLifetimeManager());
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var confg = new ConfigurationModuleCatalog();
            return new ConfigurationModuleCatalog();
        }

        protected override void ConfigureModuleCatalog()
        {           
            ModuleCatalog catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(TMSManager));
        }
    }
}
