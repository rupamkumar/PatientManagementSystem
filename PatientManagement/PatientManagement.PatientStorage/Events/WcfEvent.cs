﻿using Infrastructure.Base;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.PatientStorage.Events
{
   public class WcfEvent :  PubSubEvent<WcfProxy>
    {
    }
}
