using Microsoft.Practices.Prism.PubSubEvents;
using PatientManagement.PatientStorage.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
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

namespace PatientManagement.PatientStorage.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    [Export(typeof(SearchView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
            //DataContext = ViewModel;
        }

        
        [Import]
       public SearchViewModel ViewModel
        {
            get {return this.DataContext as SearchViewModel; }
            set { this.DataContext = value; }
        }


    }
}
