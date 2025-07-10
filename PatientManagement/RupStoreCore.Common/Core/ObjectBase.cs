using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Prism.MefExtensions;

namespace RupStoreCore.Common.Core
{
   public abstract class ObjectBase 
    {

        public static CompositionContainer Container { get; set; }
    }
}
