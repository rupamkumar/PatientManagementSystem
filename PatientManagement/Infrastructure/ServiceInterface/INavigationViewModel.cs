using Infrastructure.Base;
using PatientManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceInterface
{
  public  interface INavigationViewModel<T>
    {
        void Load(IEnumerable<T> type) ;
        //void Load(IEnumerable<T> type, WcfProxy proxy);
        void OnPatientSaved(Patient savedPatient);

        //void OnObjectSaved(T saveObject);
    }
}
