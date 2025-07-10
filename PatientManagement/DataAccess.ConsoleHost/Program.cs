using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Services;

namespace DataAccess.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostManager = new ServiceHost(typeof(ClinicManager));
            hostManager.Open();

            Console.WriteLine("Service started. Press [Enter] to exit.");
            Console.ReadLine();

            hostManager.Close();

        }
    }
}
