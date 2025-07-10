using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Base.Events
{
    public class WcfEvent<T> : PubSubEvent<T>
    {
    }
}
