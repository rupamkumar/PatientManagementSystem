using Infrastructure.Base;
using Infrastructure.DataProvider;
using PatientManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.PatientStorage.Wrapper
{
   public class SearchItemWrapper : WrapperBase<LookupItem>
    {

        public SearchItemWrapper(LookupItem wrapper) : base(wrapper)
        {

        }

       
        public string DisplayValue
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string DisplayValueOriginalValue => GetOriginalValue<string>(nameof(DisplayValue));
        public bool DisplayValueIsChanged => GetIsChanged(nameof(DisplayValue));
    }
}
