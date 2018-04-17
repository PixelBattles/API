using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.Hubs
{
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class GameHub : Hub
    {
        public GameHub()
        {
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public void GetGameInfo()
        {
            throw new NotImplementedException();
        }

        public void SubscribeChunk()
        {
            throw new NotImplementedException();
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
