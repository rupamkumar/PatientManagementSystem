using DataAccess.Repository;
using DataAccess.ServiceInterface;
using Infrastructure.DataProvider;
using PatientManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.PatientStorage.DataProvider
{
    [Export(typeof(ILookupProvider<Patient>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PatientLookupProvider : ILookupProvider<Patient>
    {
        private readonly IPatientDataProvider _patientData;

        
        public PatientLookupProvider()
         //   :this( new PatientDataProvider())
        {
        }

        [ImportingConstructor]
        public PatientLookupProvider(IPatientDataProvider patientData)
        {
            _patientData = patientData;
        }

        public async Task<IEnumerable<Patient>> LoadAsync()
        {
            var patients = await _patientData.GetAllPatientsAsync();
            return patients;
        }

        public IEnumerable<LookupItem> GetLookup(IEnumerable<Patient> patients)
        {            
            IEnumerable<LookupItem> lookup = patients.Select(p => new LookupItem
                                                        {
                                                            Id = p.PatientId,
                                                            DisplayValue = string.Format("{0} {1}", p.FirstName, p.LastName)
                                                        })
                                                         .OrderBy(o => o.DisplayValue)
                                                         .AsEnumerable();
            return lookup;
        }

        public async Task<IEnumerable<LookupItem>> GetLookupasync()
        {
            var allPatients = await _patientData.GetAllPatientsAsync();

           var lookup =  allPatients.Select(p => new LookupItem
                            {
                                Id = p.PatientId,
                                DisplayValue = string.Format("{0} {1}", p.FirstName, p.LastName)
                            })
                            .OrderBy(o => o.DisplayValue)
                            .AsEnumerable();

            return lookup;

        }

        public IEnumerable<LookupItem> GetLookup()
        {
            throw new NotImplementedException();
        }
    }
}
