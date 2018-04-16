using PixelBattles.Fluently;
using PixelBattles.Server.IntegrationTests.Clients.Api;
using Xunit;

namespace PixelBattles.Server.IntegrationTests.Tests
{
    public class ApiBasicTests : FlowTest
    {
        public ApiBasicTests()
        {
            Context
                .Setup(new ApiClient())
                .With(client => client.BackendUrl = Configuration.GetApiBaseUrl())
                .Save();
        }

        [Fact]
        public void ApiClient_GetBattles_Not_Throws_Error()
        {
            Context
                .Get<ApiClient>()
                .Transform(t => t.GetBattles())
                .Save()
                .Continue();
        }
    }
}
