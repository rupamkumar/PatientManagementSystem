using RupStoreCore.Common.Configuration;
using RupStoreCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RupStoreAccessToken
{
  public  class RupStoreAccessSettings : ConfigurationSettings
    {


        private static RupStoreAccessSettings _Instance;

        public static RupStoreAccessSettings Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new RupStoreAccessSettings();
                return _Instance;
            }
            set
            {
                _Instance = value;
            }
            
        }

        public IApplicationAccess App()
        {
            return ApplicationAccessSettings.Instance;
        }

       
        public override void LoadSettings()
        {
            
            App().GrantType = GetSetting<string>("grant_type", "");
            App().ClientId = GetSetting<string>("client_id", "");
            App().ClientSecret = GetSetting<string>("client_secret", "");
        }

    }
}
