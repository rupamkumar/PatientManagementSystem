using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RupStoreAuthentication.Model
{
   public class User : ViewModelBase
    {

        private string _UserName = string.Empty;
        private string _Password = string.Empty;

       // [Required]
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value; 
                OnPropertyChanged();

            }
        }

        //[Required]
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }
    }
}
