using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            services.AddBusinessLogic(ConfigurationRoot);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseBusinessLogic(ConfigurationRoot, loggerFactory);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
