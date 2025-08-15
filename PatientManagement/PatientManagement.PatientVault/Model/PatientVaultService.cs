using Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PatientManagement.PatientVault.Model
{
    [Export(typeof(IPatientVaultService))]
    public class PatientVaultService : IPatientVaultService
    {
        private readonly IList<Patient> patients;
        public PatientVaultService()
        {
            this.patients = new List<Patient>
            {

                new Patient
                {
                    Id = Guid.Parse("5C5BC399-F03F-4301-B314-2D70C1FF2306"),
                    LastName = "sarkar",
                    FirstName = "test",
                    DOB = DateTime.Parse("1875/05/05"),
                    Gender = "Male",
                    Email = "test.sarkar@health.wa.gov.au"
                },
                 new Patient
                {

                    Id = Guid.Parse("D84FF2F9-144C-4357-8DC7-785394FC99A6"),
                    LastName = "test",
                    FirstName = "test3",
                    DOB = DateTime.Parse("2015/05/05"),
                    Gender = "Male",
                    Email = "test3.test@primary.com.au"
                },
                  new Patient
                {
                    Id =  Guid.Parse("687E0458-A3B2-4688-8CEE-BD0E63A01C10"),
                    LastName = "test",
                    FirstName = "John",
                    DOB = DateTime.Parse("2017/05/05"),
                    Gender = "Male",
                    Email = "John.test@primary.com.au"
                }
            };
        }

        public IAsyncResult BeginGetPatients(AsyncCallback callback, object userState)
        {
            var asyncResult = new AsyncResult<IEnumerable<Patient>>(callback, userState);
            ThreadPool.QueueUserWorkItem(
                o =>
                {
                    asyncResult.SetComplete(new ReadOnlyCollection<Patient>(this.patients), false);
                });
            return asyncResult;
        }
        public IEnumerable<Patient> EndGetPatients(IAsyncResult result)
        {
            var patientAsyncResult = AsyncResult<IEnumerable<Patient>>.End(result);

            return patientAsyncResult.Result;
        }

        public Patient GetPatient(Guid id)
        {
            return this.patients.FirstOrDefault(e => e.Id == id);
        }

        private IEnumerable<Patient> CreatePatients()
        {
            List<Patient> patients = new List<Patient>
            {

                new Patient
                {
                    LastName = "McCalvey",
                    FirstName = "Peter",
                    DOB = DateTime.Parse("1875/05/05"),
                    Gender = "Male",
                    Email = "Peter.McCalvey@health.wa.gov.au"
                },
                 new Patient
                {
                    LastName = "Zaba",
                    FirstName = "Simon",
                    DOB = DateTime.Parse("2015/05/05"),
                    Gender = "Male",
                    Email = "Simon.Zaba@primary.com.au"
                },
                  new Patient
                {
                    LastName = "Beltman",
                    FirstName = "John",
                    DOB = DateTime.Parse("2017/05/05"),
                    Gender = "Male",
                    Email = "John.Beltman@primary.com.au"
                }
            };
            return patients;
        }

        
    }
}
