using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.BusinessLogic.Managers;
using PixelBattles.Server.DataStorage;

namespace PixelBattles.Server.BusinessLogic
{
    public static class BusinessLogicExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfigurationRoot configuration)
        {
            return services
                .AddDataStorage(configuration)
                .AddBattleTokenLogic(configuration)
                .AddManagers()
                .AddSingleton<ErrorDescriber>();
        }
        
        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            return services
                .AddScoped<IBattleManager, BattleManager>();
        }
    }
}
