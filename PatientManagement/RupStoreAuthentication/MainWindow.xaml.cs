using Infrastructure;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using RupStoreAuthentication.ViewModel;
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

namespace RupStoreAuthentication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export]
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //private const string PatientModuleManager = "PatientModuleManager";
        //[Import(AllowRecomposition = true)]
        //public IModuleManager ModuleManager;

        //[Import(AllowRecomposition = true)]
        //public IRegionManager RegionManager;

        //[Import]
        //[SuppressMessage("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly", Justification = "Needs to be a property to be composed by MEF")]
        //LoginViewModel ViewModel
        //{
        //    get { return this.DataContext as LoginViewModel; }
        //    set { this.DataContext = value; }
        //}

       
    }
}
