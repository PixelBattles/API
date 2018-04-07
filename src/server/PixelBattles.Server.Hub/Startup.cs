using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using PixelBattles.Server.BusinessLogic;
using PixelBattles.Server.Hubs.Utils;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PixelBattles.Server.Hubs
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

            services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(ClaimTypes.NameIdentifier);
                });
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters =
            //        new TokenValidationParameters
            //        {
            //            LifetimeValidator = (before, expires, token, parameters) => expires > DateTime.UtcNow,
            //            ValidateAudience = false,
            //            ValidateIssuer = false,
            //            ValidateActor = false,
            //            ValidateLifetime = true,
            //            IssuerSigningKey = SecurityKey
            //        };

            //        options.Events = new JwtBearerEvents
            //        {
            //            OnMessageReceived = context =>
            //            {
            //                var accessToken = context.Request.Query["access_token"];

            //                if (!string.IsNullOrEmpty(accessToken) &&
            //                    (context.HttpContext.WebSockets.IsWebSocketRequest || context.Request.Headers["Accept"] == "text/event-stream"))
            //                {
            //                    context.Token = context.Request.Query["access_token"];
            //                }
            //                return Task.CompletedTask;
            //            }
            //        };
            //    });

            services.AddAttributeRegistration();

            services.AddBusinessLogic(Configuration);

            services.AddSingleton(PixelBattleHubContextFactory.Create);

            services.AddScoped<TestHub>();

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
                routes.MapHub<TestHub>("hub/test");
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
