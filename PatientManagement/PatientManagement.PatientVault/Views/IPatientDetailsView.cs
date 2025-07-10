using PatientManagement.PatientVault.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.PatientVault.Views
{
   public interface IPatientDetailsView
    {
        PatientDetailsViewModel ViewModel { get; set; }
    }
}
