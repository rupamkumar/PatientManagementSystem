using RupStoreCore.Common.MessageBroker;
using RupStoreSplash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RupStoreAuthentication
{
 public static  class RupStoreLogin
    {

        private static Window mLogin;

        /// <summary>
        /// Get or set the splash screen window
        /// </summary>
        public static Window Login
        {
            get
            {
                return mLogin;
            }
            set
            {
                mLogin = value;
            }
        }

        public static void ShowLogin()
        {
            if (mLogin != null)
            {
                mLogin.Show();
            }
        }
        
        public static void TriggerLogin()
        {
            Login = new MainWindow();
            ShowLogin();            
        }

       

        public static void CloseLogin()
        {
            if (mLogin != null)
            {
                mLogin.Close();

                if (mLogin is IDisposable)
                    (mLogin as IDisposable).Dispose();
            }
        }
    }
}
