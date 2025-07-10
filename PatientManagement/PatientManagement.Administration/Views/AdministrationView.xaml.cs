using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PatientManagement.Administration.Views
{
    /// <summary>
    /// Interaction logic for AdministrationView.xaml
    /// </summary>
    [Export]
    public partial class AdministrationView : UserControl
    {
        public AdministrationView()
        {
            InitializeComponent();
        }
    }
}
