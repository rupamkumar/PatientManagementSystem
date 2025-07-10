using RupStoreAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RupStoreAuthentication.Contracts
{
    public interface ILoginViewModel
    {
        User UserEntity { get; set; }
    }
}
