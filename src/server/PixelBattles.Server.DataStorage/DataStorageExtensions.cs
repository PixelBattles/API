using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PixelBattles.Server.Core;
using System;

namespace PixelBattles.Server.DataStorage
{
    public static class DataStorageExtensions
    {
        public static IServiceCollection AddDataStorage(this IServiceCollection services, IConfigurationRoot configuration)
        {
            
            return services
                .AddDbContext<PixelBattlesDbContext>(options =>
                {
                    options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"],
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(DataStorageExtensions).Assembly.GetName().Name));
                });
        }

        public static void UseDataStorage(this IServiceScope serviceScope, IConfigurationRoot configuration, ILoggerFactory loggerFactory)
        {
            var context = serviceScope.ServiceProvider.GetService<PixelBattlesDbContext>();
            var logger = loggerFactory.CreateLogger("Data storage startup");

            switch (configuration["ASPNETCORE_ENVIRONMENT"])
            {
                case EnvironmentNames.Staging:
                    context.Database.Migrate();
                    try
                    {
                        logger.LogInformation(-1, "Updating database.");
                    }
                    catch (Exception exception)
                    {
                        logger.LogError(-1, exception, "Database update partially failed.");
                        throw;
                    }
                    break;
                case EnvironmentNames.Production:
                    context.Database.Migrate();
                    try
                    {
                        logger.LogInformation(-1, "Updating database.");
                    }
                    catch (Exception exception)
                    {
                        logger.LogError(-1, exception, "Database update partially failed.");
                        throw;
                    }
                    break;
                case EnvironmentNames.Development:
                    context.Database.Migrate();
                    try
                    {
                        logger.LogInformation(-1, "Updating database.");
                    }
                    catch (Exception exception)
                    {
                        logger.LogError(-1, exception, "Database update partially failed.");
                        throw;
                    }
                    break;
                default:
                    InvalidOperationException invalidOperationException = new InvalidOperationException("Unknown environment");
                    logger.LogError(-1, invalidOperationException, $"{configuration["ASPNETCORE_ENVIRONMENT"]} is unknown environment for database init.");
                    throw invalidOperationException;
            }
        }
    }
}
