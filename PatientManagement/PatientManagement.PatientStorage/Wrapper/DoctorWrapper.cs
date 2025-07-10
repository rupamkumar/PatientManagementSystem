using Infrastructure.Base;
using PatientManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.PatientStorage.Wrapper
{
   public class DoctorWrapper : WrapperBase<Doctor>
    {
        public DoctorWrapper(Doctor model) : base(model)
        {

        }

        public Guid DoctorId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }
        public Guid DoctorIdOriginalValue => GetOriginalValue<Guid>(nameof(DoctorId));
        public bool DoctorIdIsChanged => GetIsChanged(nameof(DoctorId));

        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string FirstNameOriginalValue => GetOriginalValue<string>(nameof(FirstName));
        public bool FirstNameIsChanged => GetIsChanged(nameof(FirstName));

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string LastNameOriginalValue => GetOriginalValue<string>(nameof(LastName));
        public bool LastNameIsChanged => GetIsChanged(nameof(LastName));

        public string ProviderNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string ProviderNumberOriginalValue => GetOriginalValue<string>(nameof(ProviderNumber));
        public bool ProviderNumberIsChanged => GetIsChanged(nameof(ProviderNumber));

    }
}
