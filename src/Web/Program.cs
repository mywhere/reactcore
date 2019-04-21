using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using Web.Identity;
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

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    AppIdentityDbContextSeed.SeedAsync(userManager).Wait();
                }
                catch(Exception ex)
                {
                    Log.Logger.Error(ex, "An error occured when seeding the dataContext.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, configBuilder) => {
                    var builtConfig = configBuilder.Build();
                    var appSettingsSection = builtConfig.GetSection("AppSettings");
                    var appSetting = appSettingsSection.Get<AppSettings>();

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
