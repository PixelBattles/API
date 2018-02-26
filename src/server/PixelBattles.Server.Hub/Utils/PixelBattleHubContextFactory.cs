using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PixelBattles.Server.Hub.Utils
{
    public static class PixelBattleHubContextFactory
    {
        public static PixelBattleHubContext Create(IServiceProvider serviceProvider)
        {
            using (IServiceScope serviceScope = serviceProvider.CreateScope())
            {
                IHubContext<PixelBattleHub> hubContext = serviceScope.ServiceProvider.GetRequiredService<IHubContext<PixelBattleHub>>();
                
                PixelBattleHubContext serverHub = new PixelBattleHubContext(hubContext);

                return serverHub;
            }
        }
    }
}
