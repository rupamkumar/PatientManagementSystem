using Infrastructure;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementUI
{
    [Export(typeof(IShellService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MainWindowService : IShellService
    {
        //[Import(AllowRecomposition = true)]
        private readonly IUnityContainer _container;

        
        //[Import(AllowRecomposition = true)]
        private  IRegionManager _regionManager;


        //[ImportingConstructor]
        public MainWindowService(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        //[ImportingConstructor]
        public MainWindowService()
            : this(new UnityContainer(), new RegionManager())
        {
            
           
        }

        //Avoid open multiple shell; it is not a good practice.
        public void ShowShell(IRegionManager _region, string uri)
        {
            //var shell = _container.Resolve<Shell>();
            //var scopeRegion = _region.CreateRegionManager();
            //RegionManager.SetRegionManager(shell, scopeRegion);
            _region.RequestNavigate(RegionNames.MainContentRegion, uri);
            //scopeRegion.RequestNavigate(RegionNames.ContentRegion, uri);
            //shell.Show();
        }

        public void ShowShell(string url)
        {
            throw new NotImplementedException();
        }
    }
}
