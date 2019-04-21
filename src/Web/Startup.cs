using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.Infrastructure;
using Web.Services;
using Serilog;
using Web.Setting;
using Web.Extensions.Microsoft.Extensions.DependencyInjection;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            var appSettings = this.getSettings(services);
            var secretAppSettings = new SecretAppSettings(Configuration, appSettings);

            // For development env, it use in memory database
            services.ConfigureDbContext(secretAppSettings, development: true);
            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            var appSettings = this.getSettings(services);
            var secretAppSettings = new SecretAppSettings(Configuration, appSettings);

            // For producation env, it use SQL database.
            // TODO: add connection string for data/identity in App settings.
            // TODO: support azure key vault support.
            services.ConfigureDbContext(secretAppSettings, development: false);
            ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var appSettings = this.getSettings(services);
            services.AddLogging(loggingBuilder =>
                loggingBuilder
                    .AddSerilog(dispose: true)
                    .AddAzureWebAppDiagnostics()
                );

            services.ConfigureIdentity();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddNodeServices();
            services.AddSpaPrerenderer();
            services.AddSingleton(appSettings);
            // Add your own services here.
            services.AddScoped<AccountService>();
            services.AddScoped<PersonService>();

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            // Build your own authorization system or use Identity.
            app.Use(async (context, next) =>
            {
                var accountService = context.RequestServices.GetService(typeof(AccountService)) as AccountService;
                var verifyResult = accountService.Verify(context);
                if (!verifyResult.HasErrors)
                {
                    context.Items.Add(Constants.HttpContextServiceUserItemKey, verifyResult.Value);
                }
                await next.Invoke();
                // Do logging or other work that doesn't write to the Response.
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(
                    new WebpackDevMiddlewareOptions()
                    {
                        HotModuleReplacement = true
                    });
            }
            else
            {
                app.UseExceptionHandler("/Main/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Main}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Main", action = "Index" });
            });
        }

        private AppSettings getSettings(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            return appSettingsSection.Get<AppSettings>();
        }
    }
}
