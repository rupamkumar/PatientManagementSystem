using Infrastructure;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PatientManagementUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    [Export]
    public partial class MainWindow : Window, IView
        //, IPartImportsSatisfiedNotification
    {
        private const string PatientVaultModule = "PatientsVaultModule";
        private const string moduleVaultManager = "ModuleVaultManager";
        private static Uri PatientDetailsView = new Uri("/PatientDetailsView", UriKind.Relative);
        private static Uri ViewA = new Uri("/Test", UriKind.Relative);
        private static Uri ViewB = new Uri("/ViewB", UriKind.Relative);

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModelLocationProvider.AutoWireViewModelChanged(this);           
            
        }
        

       
        [Import(AllowRecomposition = true)]
        public IModuleManager ModuleManager;

        [Import(AllowRecomposition = true)]
        public IRegionManager RegionManager;

        public void OnImportsSatisfied()
        {
           
            this.ModuleManager.LoadModuleCompleted += (s, e) =>
            {
                if (e.ModuleInfo.ModuleName == PatientVaultModule)
                {
                    this.RegionManager.RequestNavigate(
                            RegionNames.ContentRegion,
                            PatientDetailsView);
                }
                if (e.ModuleInfo.ModuleName == moduleVaultManager)
                {
                    this.RegionManager.RequestNavigate(
                            RegionNames.ContentRegion,
                            ViewA);
                }
            };
        }
        //private void Navigate(string viewName)
        //{
        //    RegionManager.RequestNavigate(RegionNames.ContentRegion, viewName);
        //}
    }
}
