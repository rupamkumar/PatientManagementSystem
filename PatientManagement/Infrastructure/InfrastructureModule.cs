using Infrastructure.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class InfrastructureModule : IModule
    {
        IRegionManager _regionManager;
        IUnityContainer _container;
        public InfrastructureModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }
        public void Initialize()
        {
            _container.RegisterType(typeof(object), typeof(ViewA), "ViewA");
        }
    }
}
