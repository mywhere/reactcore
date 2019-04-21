using System;

namespace Web.Setting
{
    public class AppSettings
    {
        public bool IsDevelopment =>
            Environment.GetEnvironmentVariables()["ASPNETCORE_ENVIRONMENT"]?.ToString() == "Development";

        public string JwtTokenSigningKey { get; set; }

        public string Host { get; set; }

        public GoogleAccountConfiguration Google { get; set; }

        public EmailConfiguration Email { get; set; }

        public KeyVaultConfiguration KeyVault { get; set; }
    }

    public class EmailConfiguration
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool EnableSSL { get; set; }
    }

    public class GoogleAccountConfiguration
    {
        public bool Enabled { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }

    public class KeyVaultConfiguration
    {
        public bool Enabled { get; set; }
        public string VaultName { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string DataConnectionStringKey { get; set; }

        public string IdentityConnectionStringKey { get; set; }
    }
}
