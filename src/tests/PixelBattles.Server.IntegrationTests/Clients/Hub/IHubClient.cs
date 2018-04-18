using PixelBattles.Shared.DataTransfer.Hub;
using System;

namespace PixelBattles.Server.IntegrationTests.Clients.Hub
{
    public interface IHubClient
    {
        event Action<Exception> Closed;

        string BackendUrl { get; set; }

        string Token { get; set; }

        void Connect();

        GameInfoDTO GetGameInfo();

        void Disconnect();
    }
}
