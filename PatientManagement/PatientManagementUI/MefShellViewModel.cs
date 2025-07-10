
using DataAccess.Contracts;
using DataAccess.Proxies;
using Infrastructure;
using Infrastructure.Base;
using Infrastructure.Base.Events;
using Infrastructure.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementUI
{
    [Export(typeof(MefShellViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MefShellViewModel : BindableBase, IRegionManagerAware
    {
        //[Import]
        [Import(AllowRecomposition = true)]
        private IShellService _service;

        private string ClientName { get; set; }

       


        public DelegateCommand<string> OpenShellCommand { get; private set; }
        public DelegateCommand<string> NavigateCommand { get; private set; }
        public IRegionManager RegionManager { get; set; }

       

        [ImportingConstructor]
        public MefShellViewModel(IShellService service)
        {            
            _service = service;
            OpenShellCommand = new DelegateCommand<string>(OpenShell);
            NavigateCommand = new DelegateCommand<string>(Navigate);
           
        }

                
        public NavigationParameters Parameters { get; set; }

        private void Navigate(string viewName)
        {            
            RegionManager.RequestNavigate(RegionNames.MainContentRegion, viewName, Parameters);           
        }

        private void OpenShell(string obj)
        {
            _service.ShowShell(RegionManager,obj);
        }
    }
}
