using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PixelBattles.API.Server.BusinessLogic.BattleToken
{
    public static class BattleTokenExtensions
    {
        public static IServiceCollection AddBattleTokenLogic(this IServiceCollection services, IConfigurationRoot configuration)
        {
            return services
                .Configure<BattleTokenOptions>(configuration.GetBattleTokenSection())
                .AddSingleton<IBattleTokenGenerator, BattleTokenGenerator>();
        }

        private static IConfigurationSection GetBattleTokenSection(this IConfiguration configuration)
        {
            return configuration
                .GetSection(nameof(BattleTokenOptions));
        }

        public static BattleTokenOptions GetBattleTokenOptions(this IConfiguration configuration)
        {
            return configuration
                .GetBattleTokenSection().Get<BattleTokenOptions>();
        }
    }
}
