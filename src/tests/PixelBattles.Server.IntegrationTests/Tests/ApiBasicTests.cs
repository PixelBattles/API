using PixelBattles.Fluently;
using PixelBattles.Server.IntegrationTests.Clients.Api;
using PixelBattles.Shared.DataTransfer.Api.Battle;
using System.Collections.Generic;
using Xunit;

namespace PixelBattles.Server.IntegrationTests.Tests
{
    public class ApiBasicTests : FlowTest
    {
        public ApiBasicTests()
        {
            FlowContext
                .Setup(new ApiClient())
                .With(client => client.BackendUrl = "http://localhost:5000")
                .Save();
        }

        [Fact]
        public void ApiClient_GetBattles_Not_Throws_Error()
        {
            FlowContext
                .Get<ApiClient>()
                .Transform(t => t.GetBattles())
                .Save()
                .Continue();
        }
    }
}
