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
    [Export(typeof(ILookupProvider<Department>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PatientDepartmentLookupProvider : ILookupProvider<Department>
    {
        //[Import]
        private readonly IPatientDataProvider _patientData;

        
        public PatientDepartmentLookupProvider()
        {

        }

        [ImportingConstructor]
        public PatientDepartmentLookupProvider(IPatientDataProvider patientData)
        {
            _patientData = patientData;
        }
        public IEnumerable<LookupItem> GetLookup()
        {            
            return null;
        }

        public IEnumerable<LookupItem> GetLookup(IEnumerable<Department> departments)
        {
            IEnumerable<LookupItem> lookup = departments.Select(d => new LookupItem
            {
                Id = d.DepartmentId,
                DisplayValue = d.Name
            })
            .OrderBy(o => o.DisplayValue)
            .AsEnumerable();

            return lookup;
        }

        public Task<IEnumerable<Department>> LoadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
