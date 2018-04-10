//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;

//namespace PixelBattles.Server.Hubs.Utils
//{
//    public static class HubExtensions
//    {
//        public static void InitializeHub(this IApplicationBuilder app, IConfigurationRoot configuration, ILogger logger)
//        {
//            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
//            {
//                serviceScope.ServiceProvider.GetRequiredService<PixelBattleHubContext>();
//            }
//        }
//    }
//}
