using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PixelBattles.Server.BusinessLogic.Managers;
using PixelBattles.Shared.DataTransfer.Hub;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.Hubs
{
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class GameHub : Hub
    {
        private PixelBattleHubContext PixelBattleHubContext { get;set;}
        
        public GameHub(PixelBattleHubContext pixelBattleHubContext)
        {
            this.PixelBattleHubContext = pixelBattleHubContext;
        }

        private Guid GetGameId()
        {
            return Guid.Parse(Context.User.FindFirst(GameTokenConstants.GameIdClaim).Value);
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public Task<GameInfoDTO> GetGameInfoAsync()
        {
            return PixelBattleHubContext.GetGameAsync(GetGameId());
        }

        public Task<bool> SubscribeChunkAsync(int x, int y)
        {
            return Task.FromResult(false);
        }

        public void UnsubscribeChunk()
        {
            throw new NotImplementedException();
        }

        public void GetChunkState()
        {
            throw new NotImplementedException();
        }

        public void ProcessAction()
        {
            throw new NotImplementedException();
        }
    }
}
