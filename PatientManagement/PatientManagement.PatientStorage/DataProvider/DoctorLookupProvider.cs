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
    [Export(typeof(ILookupProvider<Doctor>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DoctorLookupProvider : ILookupProvider<Doctor>
    {
        private readonly IDoctorDataProvider _doctorData;

        [ImportingConstructor]
        public DoctorLookupProvider(IDoctorDataProvider doctorData)
        {
            _doctorData = doctorData;
        }
        public IEnumerable<LookupItem> GetLookup(IEnumerable<Doctor> type)
        {
            IEnumerable<LookupItem> lookup = type.Select(d => new LookupItem
            {
                Id = d.DoctorId,
                DisplayValue = string.Format("{0} {1}" , d.FirstName, d.LastName)
            })
             .OrderBy(o => o.DisplayValue)
             .AsEnumerable();

            return lookup;
        }

        public IEnumerable<LookupItem> GetLookup()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Doctor>> LoadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
