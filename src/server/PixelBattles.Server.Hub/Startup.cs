using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using PixelBattles.Server.BusinessLogic;
using PixelBattles.Server.Hub.Utils;

namespace PixelBattles.Server.Hub
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; }

        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("appsettings.Custom.json", optional: true);

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();

            HostingEnvironment = env;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login/";
                });

            services.AddAutoMapper();

            services.AddSockets();

            services.AddSignalR(option =>
            {
                option.JsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddAttributeRegistration();

            services.AddBusinessLogic(Configuration);

            services.AddSingleton(PixelBattleHubContextFactory.Create);

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
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));

                app.UseDeveloperExceptionPage();
            }
            else
            {
                loggerFactory.AddDebug();
            }

            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowCredentials());

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<PixelBattleHub>("hub/game");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.InitializeHub(Configuration, loggerFactory.CreateLogger("Hub initialization"));
        }
    }
}
