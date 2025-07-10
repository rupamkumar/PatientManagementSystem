using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RupStoreCore.Common.Contracts
{
   public interface IApplicationAccess
    {
        string GrantType { get; set; }
        
        string ClientId { get; set; }
        
        string ClientSecret { get; set; }
        
        string Username { get; set; }
        
        string Password { get; set; }        

    }
}
