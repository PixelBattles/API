using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.Hub
{
    public class PixelBattleHub : Microsoft.AspNetCore.SignalR.Hub<IHubClient>
    {
        protected PixelBattleHubContext PixelBattleHubContext { get; set; }

        public PixelBattleHub(PixelBattleHubContext pixelBattleHubContext)
        {
            this.PixelBattleHubContext = pixelBattleHubContext;
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
