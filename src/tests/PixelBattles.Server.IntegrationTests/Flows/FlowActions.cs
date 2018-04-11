using PixelBattles.Server.IntegrationTests.Clients.Api;
using PixelBattles.Server.IntegrationTests.Clients.Hub;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixelBattles.Server.IntegrationTests.Flows
{
    public static class FlowActions
    {
        public static FlowContext WithHubBackend(this FlowContext context, string url)
        {
            context.HubClient = new HubClient(url);
            return context;
        }

        public static FlowContext ForGame(this FlowContext context, Guid gameId)
        {
            return context;
        }

        public static FlowContext ConnectToGame(this FlowContext context)
        {
            return context;
        }

        public static FlowContext GetGameToken(this FlowContext context)
        {
            return context;
        }
    }
}
