using Microsoft.Extensions.Configuration;

namespace PixelBattles.Server.IntegrationTests
{
    public static class ConfigurationExtensions
    {
        public static string GetApiBaseUrl(this IConfiguration configuration)
        {
            return configuration.GetSection("api")["baseUrl"];
        }

        public static string GetHubBaseUrl(this IConfiguration configuration)
        {
            return configuration.GetSection("hub")["baseUrl"];
        }
    }
}
