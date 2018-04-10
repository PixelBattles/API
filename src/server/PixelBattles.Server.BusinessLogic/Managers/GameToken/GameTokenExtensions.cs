using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public static class GameTokenExtensions
    {
        public static IServiceCollection AddGameTokenLogic(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<GameTokenOptions>(configuration.GetGameTokenSection());
            services.AddSingleton<IGameTokenGenerator, GameTokenGenerator>();

            return services;
        }

        public static IConfigurationSection GetGameTokenSection(this IConfiguration configuration)
        {
            return configuration.GetSection(GameTokenConstants.GameTokenSection);
        }

        public static GameTokenOptions GetGameTokenOptions(this IConfiguration configuration)
        {
            return configuration.GetGameTokenSection().Get<GameTokenOptions>();
        }
    }
}
