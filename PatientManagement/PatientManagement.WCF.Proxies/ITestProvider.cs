using System.ServiceModel;

namespace PatientManagement.WCF.Proxies
{
   public interface ITestProvider
    {
        InstanceContext Context { get; set; }
    }
}
