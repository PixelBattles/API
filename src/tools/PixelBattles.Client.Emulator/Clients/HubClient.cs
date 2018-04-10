using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
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

        public HubClient(ILogger logger, string hubUrl)
        {
            hubConnection = null;
            this.logger = logger;
            this.hubUrl = hubUrl;
        }
        
        public async Task ConnectAsync()
        {
            try
            {
                logger.LogInformation("Connecting to game...");

                hubConnection = new HubConnectionBuilder()
                    .WithUrl(hubUrl + "/hubs/game")
                    .WithAccessToken(() => "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJHYW1lSWQiOiJkNDlmZGMyOS1kZTZlLTQwOWItZDcwMS0wOGQ1ODk0NTFlYzgiLCJVc2VySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJleHAiOjE1MjM0NzY0MjYsImlzcyI6IlBpeGVsQmF0dGxlc1NlcnZlciIsImF1ZCI6IlBpeGVsQmF0dGxlc0NsaWVudCJ9.bO8JD3Y0xxMh4gWg3b3Si2mPcLWzBwzDBIs6iYWXI8o")
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
                logger.LogError($"Failed connect to game.", exception);
            }
        }

        public async Task DisconnectAsync()
        {
            try
            {
                await hubConnection.StopAsync();
                await hubConnection.DisposeAsync();
                hubConnection = null;
            }
            catch (Exception exception)
            {
                logger.LogError($"Failed disconnect.", exception);
            }
        }
    }
}
