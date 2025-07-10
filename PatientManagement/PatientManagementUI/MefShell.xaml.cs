
using DataAccess.Contracts;
using Infrastructure;
using Infrastructure.Base;
using Infrastructure.Base.Events;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace PatientManagementUI
{
    /// <summary>
    /// Interaction logic for MefShell.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MefShell : MetroWindow, IView, IPartImportsSatisfiedNotification
    {
        private const string PatientModuleManager = "PatientModuleManager";
        private const string PatientsVaultModule = "PatientsVaultModule";
        private static Uri PatientDetailsView = new Uri("/MainView", UriKind.Relative);
        private static Uri PatientView = new Uri("/PatientView", UriKind.Relative);
        private static Uri ViewA = new Uri("/MainView", UriKind.Relative);
        public MefShell()
        {
            InitializeComponent();
            ClientName = Application.Current.MainWindow.Title + Guid.NewGuid();
            RegisterClient(ClientName);
        }

        

        [Import]
        [SuppressMessage("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly", Justification = "Needs to be a property to be composed by MEF")]
        MefShellViewModel ViewModel
        {
            get { return this.DataContext as MefShellViewModel; }
            set { this.DataContext = value; }
        }

        private NavigationParameters parameters = new NavigationParameters();
        [Import(AllowRecomposition = true)]
        public IModuleManager ModuleManager;

        [Import(AllowRecomposition = true)]
        public IRegionManager RegionManager;

        
        private string ClientName { get; set; }

        public void OnImportsSatisfied()
        {
            

            this.ModuleManager.LoadModuleCompleted += (s, e) =>
            {
               
                if (e.ModuleInfo.ModuleName == PatientModuleManager)
                {
                    this.RegionManager.RequestNavigate(
                            RegionNames.MainContentRegion,
                            PatientView,parameters);
                }
                //if (e.ModuleInfo.ModuleName == moduleVaultManager)
                //{
                //    this.RegionManager.RequestNavigate(
                //            RegionNames.ContentRegion,
                //            ViewA);
                //}
            };
            ViewModel.Parameters = parameters;


        }

        
        private void RegisterClient(string title)
        {
            //RegisterClient(title,  out wcfProxy);
            parameters.Add("clientName", title);          

        }

        //private delegate void HandleBroadcastCallback(object sender, EventArgs e);
        //public void HandleBroadcast(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var eventData = (EventDataType)sender;
        //        if (!string.IsNullOrEmpty(eventData.EventMessage))
        //        {
        //            //this.txtEventMessages.Text = (string.Format("{0} (from {1})",
        //            //eventData.EventMessage, eventData.ClientName));
        //        }
        //        else
        //        {
        //            //this.txtEventMessages.Text = string.Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}




    }
}
