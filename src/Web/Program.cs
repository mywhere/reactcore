using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Web.Setting;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.AzureApp()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            var host = CreateWebHostBuilder(args).Build();

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, configBuilder) => {
                    var builtConfig = configBuilder.Build();
                    var appSettingsSection = builtConfig.GetSection("AppSettings");
                    var appSetting = appSettingsSection.Get<AppSetting>();

                    if (appSetting.KeyVault.Enabled)
                    {
                        configBuilder.AddAzureKeyVault(
                            $"https://{appSetting.KeyVault.VaultName}.vault.azure.net/",
                            appSetting.KeyVault.ClientId,
                            appSetting.KeyVault.ClientSecret);
                    }
                })
                .UseStartup<Startup>();
    }
}
