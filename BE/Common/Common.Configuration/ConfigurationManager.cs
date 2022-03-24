using Microsoft.Extensions.Configuration;

namespace Common.Configuration
{
    public static class ConfigurationManager
    {
        public static AppSettingsContainer AppSettings { get; private set; }

        static ConfigurationManager()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                var config = builder.Build();
                AppSettings = new AppSettingsContainer(config);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Не удалось прочитать файл appsettings.json");
            }
        }
    }
}