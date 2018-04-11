using PixelBattles.Server.IntegrationTests.Clients.Api;
using PixelBattles.Server.IntegrationTests.Clients.Hub;

namespace PixelBattles.Server.IntegrationTests.Flows
{
    public class FlowContext
    {
        public IApiClient ApiClient { get; set; }
        
        public IHubClient HubClient { get; set; }
    }
}
