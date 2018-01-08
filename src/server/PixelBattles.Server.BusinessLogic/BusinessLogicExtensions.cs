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
                .AddDataStorage(configuration)
                .AddSingleton<ErrorDescriber>();
        }
    }
}
