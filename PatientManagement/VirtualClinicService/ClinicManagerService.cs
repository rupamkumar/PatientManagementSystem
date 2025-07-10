using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace VirtualClinicService
{
    public partial class ClinicManagerService : ServiceBase
    {
        ServiceHost hostManager = new ServiceHost(typeof(ClinicManager));
        public ClinicManagerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            hostManager.Open();
        }

        protected override void OnStop()
        {
            hostManager.Close();
        }
    }
}
