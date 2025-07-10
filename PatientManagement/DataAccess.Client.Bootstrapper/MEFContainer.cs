using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using DataAccess.Proxies;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Proxies.ServiceProxies;

namespace DataAccess.Client.Bootstrapper
{
    public static class MEFContainer
    {
        public static CompositionContainer Init()
        {
            return Init(null);
        }

        public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogParts)
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(BroadcastClient).Assembly));

            if(catalogParts != null)
            {
                catalogParts.ToList().ForEach(part => catalog.Catalogs.Add(part));
            }

            CompositionContainer container = new CompositionContainer(catalog);
            return container;
        }
    }
}
