using RupStoreCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RupStoreCore.Common.Configuration
{
    public class ApplicationAccessSettings : IApplicationAccess
    {
        private string _grantType;
        private string _clientId;
        private string _clientSecret;
        private string _username;
        private string _password;
        public string GrantType { get => _grantType; set => _grantType = value; }
        public string ClientId { get => _clientId; set => _clientId = value; }
        public string ClientSecret { get => _clientSecret; set => _clientSecret = value; }
        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }

        private static ApplicationAccessSettings _Instance;

        public static ApplicationAccessSettings Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ApplicationAccessSettings();
                return _Instance;
            }
            set
            {
                _Instance = value;
            }

        }
    }
}
