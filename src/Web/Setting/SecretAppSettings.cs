using Microsoft.Extensions.Configuration;

namespace Web.Setting
{
    public class SecretAppSettings
    {
        private readonly IConfiguration configuration;
        private readonly AppSetting appSettings;

        public SecretAppSettings(IConfiguration config, AppSetting appSettings)
        {
            this.configuration = config;
            this.appSettings = appSettings;
        }

        public string DataConnectionString
        {
            get
            {
                if (this.appSettings.KeyVault.Enabled)
                {
                    return this.configuration[this.appSettings.KeyVault.DataConnectionStringKey];
                }
                return null;
            }
        }

        public string IdentityConnectionString
        {
            get
            {
                if (this.appSettings.KeyVault.Enabled)
                {
                    return this.configuration[this.appSettings.KeyVault.IdentityConnectionStringKey];
                }
                return null;
            }
        }
    }
}
