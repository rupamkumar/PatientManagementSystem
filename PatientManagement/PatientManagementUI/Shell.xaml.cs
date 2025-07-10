using Infrastructure;
using MahApps.Metro.Controls;
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
    /// Interaction logic for Shell.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class Shell : MetroWindow, IView, IPartImportsSatisfiedNotification
    {
        private const string PatientVaultModule = "PatientsVaultModule";
        private static Uri PatientDetailsView = new Uri("/PatientNavigationItemView", UriKind.Relative);
        public Shell()
        {
            InitializeComponent();
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
                            RegionNames.MainContentRegion,
                            PatientDetailsView);
                }
            };
        }
    }
}
