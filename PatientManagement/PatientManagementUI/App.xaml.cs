using PatientManagementUI.Startup;
using RupStoreSplash;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PatientManagementUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            
            MBootstrapper bootstrapper = new MBootstrapper();
            bootstrapper.Run();
        }
    }
}
