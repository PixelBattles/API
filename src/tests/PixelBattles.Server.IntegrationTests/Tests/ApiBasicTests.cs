using PixelBattles.Fluently;
using PixelBattles.Server.IntegrationTests.Clients;
using PixelBattles.Server.IntegrationTests.Clients.Api;
using PixelBattles.Shared.DataTransfer.Api.Battle;
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
                .Save()
                .Continue()
                .Setup(new NameGenerator())
                .Save()
                .Continue();
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

        [Fact]
        public void ApiClient_Can_Create_NewBattle()
        {
            Context
                .Get<ApiClient>()
                .Transform(client => client.CreateBattle(new CreateBattleDTO()
                {
                    Name = Context.Get<NameGenerator>().Value.GenerateBattleName(),
                    Description = ""
                }))
                .Save()
                .Continue()
                .Assert().Equals(c => c.Get<CreateBattleResultDTO>().Value.Succeeded, true);
        }
    }
}
