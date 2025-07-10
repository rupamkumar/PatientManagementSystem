using PatientManagement.PatientVault.ViewModels;
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

namespace PatientManagement.PatientVault.Views
{
    /// <summary>
    /// Interaction logic for PatientView.xaml
    /// </summary>
    [Export("PatientView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PatientView : UserControl
    {
        public PatientView()
        {
            InitializeComponent();
        }

        [Import]
        public PatientViewModel ViewModel
        {
            set { this.DataContext = value; }
        }

    }
}
