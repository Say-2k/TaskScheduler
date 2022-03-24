using System;
using Microsoft.Extensions.Configuration;

namespace Common.Configuration
{
    public class AppSettingsContainer
    {
        public AppSettingsContainer(IConfigurationRoot appSettings)
        {
            this.appSettings = appSettings;
        }

        public IConfigurationRoot appSettings { get; set; }

        public string this[string key]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    throw new ArgumentNullException(nameof(key));
                }
                var value = this.appSettings[key];

                if (value is null)
                {
                    throw new ApplicationException($"Значение с ключём [{key}] не найдено");
                }
                return this.appSettings[key];
            }
        }

        public bool Contain(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }
            
            return appSettings[key] != null;
        }
    }
}
