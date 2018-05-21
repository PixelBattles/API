using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PixelBattles.Server.Client
{
    public static class ApiClientExtensions
    {
        public static IServiceCollection AddApiClient(this IServiceCollection services, IConfigurationRoot configuration)
        {
            return services
                .AddScoped<IApiClient, ApiClient>()
                .AddSingleton<ApiClientOptions>(configuration.GetSection(nameof(ApiClientOptions)).Get<ApiClientOptions>());
        }
    }
}
