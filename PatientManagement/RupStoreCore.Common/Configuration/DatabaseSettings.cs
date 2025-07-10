using RupStoreCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RupStoreCore.Common.Configuration
{
    public class DatabaseSettings : ConfigurationSettings, IDatabaseSettings
    {
        private string _dataConnection;
        public string DataConnection { get => _dataConnection; set => _dataConnection = value; }


        private static DatabaseSettings _Instance;

        public static DatabaseSettings Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new DatabaseSettings();
                return _Instance;
            }
            set
            {
                _Instance = value;
            }

        }

        public IDatabaseSettings App()
        {
            return DatabaseSettings.Instance;
        }

        public override void LoadSettings()
        {
            App().DataConnection = GetSetting<string>("DataConnection", "");
        }

        }
}
