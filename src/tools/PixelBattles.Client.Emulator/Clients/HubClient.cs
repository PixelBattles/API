using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Client.Emulator
{
    public sealed class HubClient
    {
        private string hubUrl;
        private ILogger logger;
        private HubConnection hubConnection;
        private Guid? gameId;

        public HubClient(ILogger logger, string hubUrl)
        {
            gameId = null;
            hubConnection = null;
            this.logger = logger;
            this.hubUrl = hubUrl;
        }
        
        public async Task ConnectAsync(Guid gameId)
        {
            try
            {
                logger.LogInformation("Connecting to game...");

                this.gameId = gameId;

                hubConnection = new HubConnectionBuilder()
                    .WithUrl(hubUrl)
                    .WithAccessToken(() => "")
                    .WithConsoleLogger()
                    .WithTransport(TransportType.WebSockets)
                    .Build();

                hubConnection.Closed += exception =>
                {
                    logger.LogInformation($"Connection closed with error: {exception}");
                };
                
                await hubConnection.StartAsync();

                logger.LogInformation("Connecting to game...");
            }
            catch (Exception exception)
            {
                logger.LogError($"Failed connect to {gameId} game.", exception);
            }
        }

        public async Task DisconnectAsync()
        {
            try
            {
                await hubConnection.StopAsync();
                await hubConnection.DisposeAsync();
                hubConnection = null;
                gameId = null;
            }
            catch (Exception exception)
            {
                logger.LogError($"Failed disconnect.", exception);
            }
        }
    }
}
