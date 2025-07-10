using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.WCF.Proxies
{
    [Export(typeof(ITestProvider))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TestProvider : ITestProvider
    {
       // [ImportingConstructor]
        public TestProvider()
        {
        }
        public InstanceContext Context { get ; set ; }
    }
}
