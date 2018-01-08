using Microsoft.AspNetCore.SignalR;

namespace PixelBattles.Server.Hub
{
    public class PixelBattleHubContext
    {
        protected IHubContext<PixelBattleHub> HubContext { get; set; }

        public PixelBattleHubContext(
            IHubContext<PixelBattleHub> hubContext)
        {
            this.HubContext = hubContext;
        }
    }
}
