using Infrastructure.Base;
using Microsoft.Practices.Prism.PubSubEvents;
using System;

namespace PatientManagement.PatientStorage.Events
{
    public class PatientEditViewEvent : PubSubEvent<Guid>
    {
        internal void Publish(Guid patientId, WcfProxy proxy)
        {            
        }
    }
}
