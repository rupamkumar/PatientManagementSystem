using Microsoft.Practices.Prism.PubSubEvents;
using PatientManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.PatientStorage.Events
{
   public class PatientSavedEvent : PubSubEvent<Patient>
    {
    }
}
