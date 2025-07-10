using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataProvider
{
  public  interface ILookupProvider<T>
    {       
        IEnumerable<LookupItem> GetLookup(IEnumerable<T> type);
        IEnumerable<LookupItem> GetLookup();
        Task<IEnumerable<T>> LoadAsync();
    }
}
