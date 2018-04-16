using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixelBattles.Server.IntegrationTests
{
    public static class ConfigurationExtensions
    {
        public static string GetApiBaseUrl(this IConfiguration configuration)
        {
            return configuration.GetSection("api")["baseUrl"];
        }
    }
}
