using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using PixelBattles.Server.BusinessLogic;

namespace PixelBattles.Server.Web
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; }

        public IConfigurationRoot ConfigurationRoot { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("appsettings.Custom.json", optional: true);

            builder.AddEnvironmentVariables();

            ConfigurationRoot = builder.Build();

            HostingEnvironment = env;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddBusinessLogic(ConfigurationRoot);

            services.AddAutoMapper();

            services.AddMvc(options => { })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddRazorOptions(options =>
                {
                });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug();
                loggerFactory.AddConsole(ConfigurationRoot.GetSection("Logging"));

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                loggerFactory.AddDebug();
            }
            
            app.UseBusinessLogic(ConfigurationRoot, loggerFactory);

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "landing",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "dashboard",
                    template: "game/{*.}",
                    defaults: new { controller = "Dashboard", action = "Index" });

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Dashboard", action = "Index" });
            });
        }
    }
}
