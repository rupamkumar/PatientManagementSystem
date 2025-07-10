using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Infrastructure;
using System.ComponentModel.Composition;

namespace PatientManagement.PatientVault.Views
{
    /// <summary>
    /// Interaction logic for PatientNavigationItemView.xaml
    /// </summary>
    [Export]
    public partial class PatientNavigationItemView : UserControl, IPartImportsSatisfiedNotification
    {
        private static Uri patientsViewUri = new Uri("/PatientView", UriKind.Relative);
        [Import]
        public IRegionManager regionManager;

        //[Import]
        //public IPatientDetailsView _patientDetailsView;

        public PatientNavigationItemView()
        {
            InitializeComponent();
        }

        public void OnImportsSatisfied()
        {
            IRegion mainContentRegion = this.regionManager.Regions[RegionNames.MainContentRegion];
            if(mainContentRegion != null && mainContentRegion.NavigationService != null)
            {
                mainContentRegion.NavigationService.Navigated += this.MainContentRegion_Navigated;
            }
        }

        private void MainContentRegion_Navigated(object sender, RegionNavigationEventArgs e)
        {
            this.UpdateNavigationButtonState(e.Uri);
        }

        private void UpdateNavigationButtonState(Uri uri)
        {
            this.NavigateToItemRadioButton.IsChecked = (uri == patientsViewUri);
        }

        private void NavigateToItemRadioButton_Click(object sender, RoutedEventArgs e)
        {
            this.regionManager.RequestNavigate(RegionNames.MainContentRegion, patientsViewUri);
        }
    }
}
