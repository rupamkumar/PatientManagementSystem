
using System.ComponentModel.Composition;
using PatientManagement.PatientVault.ViewModels;

using System.Windows.Controls;


namespace PatientManagement.PatientVault.Views
{
    /// <summary>
    /// Interaction logic for PatientDetailsView.xaml
    /// </summary>
    [Export("PatientDetailsView")]
    //[Export(typeof(IPatientDetailsView))]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PatientDetailsView : UserControl, IPatientDetailsView
    {
        public PatientDetailsView()
        {
            InitializeComponent();
        }
        [Import]
        public PatientDetailsViewModel ViewModel
        {
            get { return this.DataContext as PatientDetailsViewModel; }
            set { this.DataContext = value; }
        }
        
    }
}
