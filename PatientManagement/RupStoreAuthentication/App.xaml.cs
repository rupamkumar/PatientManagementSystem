
using Microsoft.Practices.Unity;
using RupStoreAccessToken;
using RupStoreAuthentication.Contracts;
using RupStoreAuthentication.Startup;
using RupStoreAuthentication.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RupStoreAuthentication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            //Bootstrapper bootstrapper = new Bootstrapper();
            //bootstrapper.Run();
            base.OnStartup(e);

            Bootstrapper.Initialize();
        }
    }
}
