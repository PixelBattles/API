using System;
using System.Collections.Generic;
using System.Text;

namespace PixelBattles.Server.IntegrationTests.Flows
{
    public static class FlowActions
    {
        public static FlowContext WithApiBackend(this FlowContext context, string url)
        {
            return context;
        }

        public static FlowContext WithHubBackend(this FlowContext context, string url)
        {
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
