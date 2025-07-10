using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RupStoreSplash
{
   public static class Splasher
    {

        /// <summary>
        /// 
        /// </summary>
        private static Window mSplash;

        /// <summary>
        /// Get or set the splash screen window
        /// </summary>
        public static Window Splash
        {
            get
            {
                return mSplash;
            }
            set
            {
                mSplash = value;
            }
        }

        /// <summary>
        /// Show splash screen
        /// </summary>
        public static void ShowSplash()
        {
            if (mSplash != null)
            {
                mSplash.Show();
            }
        }

        public static void TriggerSplash()
        {
            Splash = new SplashScreen();
            ShowSplash();

            for (int i = 0; i < 1500; i++)
            {
                MessageListener.Instance.ReceiveMessage(string.Format("Load module {0}", i));
                Thread.Sleep(5);
            }
            CloseSplash();
            
        }

        public static void TriggerSplash(AggregateCatalog Catalogs)
        {
            Splash = new SplashScreen();
            ShowSplash();
            
            foreach(var catalog in Catalogs.Catalogs)
            {
                if (catalog.GetType() == typeof(AssemblyCatalog))
                {
                    foreach (var part in catalog)
                    {
                        if(catalog.Parts.Count() > 0)
                        {
                            for (int i = 0; i < catalog.Parts.Count(); i++)
                            {
                                MessageListener.Instance.ReceiveMessage(string.Format("Load module {0}", catalog.Parts.ToList()[i]));
                                Thread.Sleep(50);
                            }
                        }                        
                    }
                }
            }
            CloseSplash();

        }
        /// <summary>
        /// Close splash screen
        /// </summary>
        public static void CloseSplash()
        {
            if (mSplash != null)
            {
                mSplash.Close();

                if (mSplash is IDisposable)
                    (mSplash as IDisposable).Dispose();
            }
        }
    }
}
