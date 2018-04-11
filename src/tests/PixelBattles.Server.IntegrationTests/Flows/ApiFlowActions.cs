using PixelBattles.Server.IntegrationTests.Clients.Api;

namespace PixelBattles.Server.IntegrationTests.Flows
{
    public static class ApiFlowActions
    {
        public static FlowContext WithApiBackend(this FlowContext context, string url)
        {
            context.ApiClient = new ApiClient(url);
            return context;
        }
    }
}
