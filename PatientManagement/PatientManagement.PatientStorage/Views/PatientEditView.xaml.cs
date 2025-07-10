
using DataAccess.Contracts;
using PatientManagement.PatientStorage.ViewModel;
using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace PatientManagement.PatientStorage.Views
{
    /// <summary>
    /// Interaction logic for PatientEditView.xaml
    /// </summary>
    [Export(typeof(PatientEditView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PatientEditView : UserControl
    {
        public PatientEditView()
        {
            InitializeComponent();
        }

        [Import]
        public PatientEditViewModel ViewModel
        {
            get { return this.DataContext as PatientEditViewModel; }
            set { this.DataContext = value; }
        }

       
    }
}
