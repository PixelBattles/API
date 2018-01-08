using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PixelBattles.Server.DataStorage;

namespace PixelBattles.Server.BusinessLogic
{
    public static class BusinessLogicExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfigurationRoot configuration)
        {
            return services
                .AddDataStorage(configuration);
        }

        public static IApplicationBuilder UseBusinessLogic(this IApplicationBuilder applicationBuilder, IConfigurationRoot configuration, ILoggerFactory loggerFactory)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.UseDataStorage(configuration, loggerFactory);
            }
            return applicationBuilder;
        }
    }
}
