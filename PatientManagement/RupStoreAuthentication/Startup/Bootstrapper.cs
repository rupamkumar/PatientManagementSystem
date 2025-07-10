
using Microsoft.Practices.Unity;
using RupStoreAccessToken;
using RupStoreAuthentication.Contracts;
using RupStoreAuthentication.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RupStoreAuthentication.Startup
{
    public static class Bootstrapper 
    {      
        public static void Initialize()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ILoginViewModel, LoginViewModel>();
            container.RegisterType<IAccessTokenService, AccessTokenService>();
            //container.RegisterType<IConfiguration, Configuration>();
            //container.Resolve<AccessTokenService>();
            //container.Resolve<Configuration>();
            var loginmodel = container.Resolve<LoginViewModel>();

            var window = new MainWindow { DataContext = loginmodel };
            window.Show();
        }
    }
}
