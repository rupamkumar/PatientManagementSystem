using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Repository;
using DataAccess.ServiceInterface;
using Infrastructure;
using PatientManagement.Model;

namespace PatientManagement.PatientVault.DataProvider
{ 

    [Export(typeof(IPatientDataProvider))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PatientDataProvider : IPatientDataProvider
    {
        //private readonly Func<IPatientRepository> _dataserviceCreator;

        //private readonly IList<Patient> patients;

        private IPatientRepository _repo;

        [ImportingConstructor]
        public PatientDataProvider()
            :this(new PatientRepository("PmsModel"))
        {
        }
        public PatientDataProvider(IPatientRepository repo)
        {
            _repo = repo;
        }

        //[ImportingConstructor]
        //public PatientDataProvider(Func<IPatientRepository> dataServiceCreator)
        //{
        //    _dataserviceCreator = dataServiceCreator;
        //}
        public Patient Add(Patient patient)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _repo.GetAllPatients();
        }

        public Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return _repo.GetAllPatientsAsync();
        }

        public Patient GetPatientById(Guid? Id)
        {
            throw new NotImplementedException();
        }

        public void RemovePatient(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void SavePatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Patient Update(Patient patient)
        {
            throw new NotImplementedException();
        }


        public IAsyncResult BeginGetPatients(AsyncCallback callback, object userState)
        {
            var asyncResult = new AsyncResult<IEnumerable<Patient>>(callback, userState);
            ThreadPool.QueueUserWorkItem(
                o =>
                {
                    asyncResult.SetComplete(new ReadOnlyCollection<Patient>(GetAllPatients().ToList()), false);
                });
            return asyncResult;
        }
        public IEnumerable<Patient> EndGetPatients(IAsyncResult result)
        {
            var patientAsyncResult = AsyncResult<IEnumerable<Patient>>.End(result);

            return patientAsyncResult.Result;
        }
    }
}
