using Infrastructure;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PatientManagement.PatientVault.DataProvider;

namespace PatientManagement.PatientVault.Views
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    [Export]
    public partial class MainView : UserControl
    {
        [Import]
        public IRegionManager regionManager;

        [Import]
        public IPatientDataProvider _repo { get; set; }
        //[ImportingConstructor]
        public MainView()
        {
            InitializeComponent();
        }

        //public void OnImportsSatisfied()
        //{
        //    IRegion mainContentRegion = this.regionManager.Regions[RegionNames.MainContentRegion];
        //}
    }
}
