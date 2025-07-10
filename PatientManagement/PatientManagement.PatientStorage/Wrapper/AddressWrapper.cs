using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using PatientManagement.Model;

namespace PatientManagement.PatientStorage.Wrapper
{
  public  class AddressWrapper : WrapperBase<Address>
    {
        public AddressWrapper(Address model) : base(model)
        {

        }

        public Guid AddressId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }
        public Guid AddressIdOriginalValue => GetOriginalValue<Guid>(nameof(AddressId));
        public bool AddressIdIsChanged => GetIsChanged(nameof(AddressId));

        public string StreetNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string StreetNumberOriginalValue => GetOriginalValue<string>(nameof(StreetNumber));
        public bool StreetNumberIsChanged => GetIsChanged(nameof(StreetNumber));

        public string Street
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string StreetOriginalValue => GetOriginalValue<string>(nameof(Street));
        public bool StreetIsChanged => GetIsChanged(nameof(Street));

        public string City
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string CityOriginalValue => GetOriginalValue<string>(nameof(City));
        public bool CityIsChanged => GetIsChanged(nameof(City));
    }
}
