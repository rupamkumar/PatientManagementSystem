using Infrastructure;
using Infrastructure.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementUI
{
   // [Export]
    public class MainWindowViewModel : BindableBase, IRegionManagerAware
    {
        [Import]
        public readonly IShellService _service;

       
        //[Import]
        public IRegionManager RegionManager { get; set; }
        public DelegateCommand<string> OpenShellCommand { get; private set; }
        public DelegateCommand<string> NavigateCommand { get; private set; }

        //[ImportingConstructor]
        public MainWindowViewModel(IShellService service) 
        {
            _service = service;
            OpenShellCommand = new DelegateCommand<string>(OpenShell);
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }
        //public MainWindowViewModel() : this(null)
        //{
           
        //}

       
        private void Navigate(string viewName)
        {
            RegionManager.RequestNavigate(RegionNames.MainContentRegion, viewName);
        }

        private void OpenShell(string viewName)
        {
            _service.ShowShell(viewName);
        }

    }
}
