using System;

namespace PixelBattles.Server.IntegrationTests.Flows
{
    public class BasicFlow
    {
        public BasicFlow()
        { }

        public void CanConnectToGame()
        {
            new FlowContext()
                .WithApiBackend("")
                .WithHubBackend("")
                .ForGame(Guid.Empty)
                .GetGameToken()
                .ConnectToGame();
        }
    }
}
