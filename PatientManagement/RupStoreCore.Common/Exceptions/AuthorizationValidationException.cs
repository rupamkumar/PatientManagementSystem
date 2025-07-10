using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RupStoreCore.Common.Exceptions
{
  public  class AuthorizationValidationException : ApplicationException
    {
        public AuthorizationValidationException(string message)
            : base(message, null)
        {
        }
        public AuthorizationValidationException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}
