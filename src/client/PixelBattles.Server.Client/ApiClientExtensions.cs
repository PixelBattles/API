using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PixelBattles.Server.Client
{
    public static class ApiClientExtensions
    {
        public static IServiceCollection AddApiClient(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var options = configuration.GetSection(nameof(ApiClientOptions)).Get<ApiClientOptions>();
            return AddApiClient(services, options);
        }

        public static IServiceCollection AddApiClient(this IServiceCollection services, Action<ApiClientOptions> configureOptions)
        {
            var options = new ApiClientOptions();
            configureOptions(options);
            return AddApiClient(services, options);
        }

        public static IServiceCollection AddApiClient(this IServiceCollection services, ApiClientOptions options)
        {
            return services
                .AddScoped<IApiClient, ApiClient>()
                .AddSingleton<ApiClientOptions>(options);
        }
    }
}
