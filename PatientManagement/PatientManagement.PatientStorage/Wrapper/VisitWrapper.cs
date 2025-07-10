using Infrastructure.Base;
using PatientManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.PatientStorage.Wrapper
{
  public  class VisitWrapper : WrapperBase<Visit>
    {
        public VisitWrapper(Visit model) :base (model)
        {                
        }

        public Guid VisitId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }
        public Guid VisitIdOriginalValue => GetOriginalValue<Guid>(nameof(VisitId));
        public bool VisitIdIsChanged => GetIsChanged(nameof(VisitId));

        [Required(ErrorMessage="Note is required")]
        public string Notes
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string NotesOriginalValue => GetOriginalValue<string>(nameof(Notes));
        public bool NotesIsChanged => GetIsChanged(nameof(Notes));

        public DoctorWrapper Doctor { get; private set; }

        protected override void InitializeComplexProperties(Visit model)
        {
            if (model.Doctor == null)
            {
                model.Doctor = new Doctor();
                //throw new ArgumentException("Doctor cannot be null");
            }
            Doctor = new DoctorWrapper(model.Doctor);
            RegisterComplex(Doctor);
        }

    }
}
