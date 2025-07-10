using DataAccess.Client.Bootstrapper;
using Infrastructure.Prism;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using RupStoreAuthentication;
using RupStoreCore.Common.Core;
using RupStoreCore.Common.MessageBroker;
using RupStoreSplash;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.Linq;
using System.Reflection;
using log4net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PatientManagementUI.Startup
{
   public class MBootstrapper : MefBootstrapper
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public MBootstrapper()
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
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MBootstrapper).Assembly));
            ////this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(PatientDataProvider).Assembly));
            ObjectBase.Container = MEFContainer.Init(new List<ComposablePartCatalog>(){
                (new AssemblyCatalog(typeof(MBootstrapper).Assembly))
                //(new AssemblyCatalog(typeof(TestProvider).Assembly))
                });
        }
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var module = new ConfigurationModuleCatalog();
            return new ConfigurationModuleCatalog();
        }

        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<MefShell>();           
        }

        
        
        protected override void InitializeShell()
        {
            base.InitializeShell();
            var regionManager = RegionManager.GetRegionManager(Shell);
            RegionManagerAware.SetRegionManagerAware(Shell, regionManager);
            MessageBroker.Instance.MessageReceived += Instance_MessageReceived;
            RupStoreLogin.TriggerLogin();
            /*
            if (_loginSuccess)
            {
                Splasher.TriggerSplash(this.AggregateCatalog);
                App.Current.MainWindow.Show();
                log.Info("This test log");
            }*/

        }

        private  void Instance_MessageReceived(object sender, MessageBrokerEventArgs e)
        {
            switch (e.MessageName)
            {
                case MessageBrokerMessages.LOGIN_SUCCESS:
                    RupStoreLogin.CloseLogin();
                    Splasher.TriggerSplash(this.AggregateCatalog);
                    App.Current.MainWindow.Show();
                    break;

                case MessageBrokerMessages.LOGIN_FAIL:
                    MessageBox.Show(MessageBrokerMessages.LOGIN_FAIL);
                    
                    break;
            }

        }
    }
}
