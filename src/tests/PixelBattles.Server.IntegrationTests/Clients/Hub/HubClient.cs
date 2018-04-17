using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.IntegrationTests.Clients.Hub
{
    public class HubClient : IHubClient
    {
        public event Action<Exception> Closed;

        public string BackendUrl { get; set; }

        public string Token { get; set; }

        public HubConnection hubConnection;

        public HubClient()
        {

        }

        public Task ConnectAsync()
        {
            hubConnection = new HubConnectionBuilder()
                     .WithUrl(BackendUrl)
                     .WithAccessToken(() => Token)
                     .WithTransport(TransportType.WebSockets)
                     .Build();

            hubConnection.Closed += exception =>
            {
                Closed?.Invoke(exception);
            };

            return hubConnection.StartAsync();
        }

        public void Connect()
        {
            ConnectAsync().Wait();
        }

        public async Task DisconnectAsync()
        {
            if (hubConnection != null)
            {
                await hubConnection.StopAsync();
                await hubConnection.DisposeAsync();
                hubConnection = null;
            }
        }

        public void Disconnect()
        {
            DisconnectAsync().Wait();
        }
    }
}
