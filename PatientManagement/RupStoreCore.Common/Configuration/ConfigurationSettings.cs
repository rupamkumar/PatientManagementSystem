using RupStoreCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RupStoreCore.Common.Configuration
{
  public  class ConfigurationSettings : IConfigurationSettings
    {

        public virtual void LoadSettings()
        {

        }

        protected T GetSetting<T>(string key, object defaultValue)
        {
            T result = default(T);
            string value;
            value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value))
            {
                result = (T)defaultValue;
            }
            else
            {
                result = (T)Convert.ChangeType(value, typeof(T));
            }

            return result;
        }

        T IConfigurationSettings.GetSetting<T>(string key, object defaultValue)
        {
            return GetSetting<T>(key, defaultValue);
        }
    }
}
