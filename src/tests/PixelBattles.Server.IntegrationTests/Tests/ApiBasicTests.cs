using PixelBattles.Fluently;
using PixelBattles.Server.IntegrationTests.Clients;
using PixelBattles.Server.IntegrationTests.Clients.Api;
using PixelBattles.Shared.DataTransfer.Api.Battle;
using System;
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
                    .Select(t => t.GetBattles())
                    .Save()
                    .Continue();
        }

        [Fact]
        public void ApiClient_Can_Create_NewBattle()
        {
            Context
                .Get<ApiClient>()
                    .Select(client => client.CreateBattle(new CreateBattleDTO()
                    {
                        Name = Context.Get<NameGenerator>().Value.GenerateBattleName(),
                        Description = ""
                    }))
                    .Save()
                    .Assert()
                        .True(t => t.Succeeded)
                        .Empty(t => t.Errors)
                        .NotNull(t => t.BattleId);
        }

        [Fact]
        public void ApiClient_Create_Battle_And_Get_Return_Same_Battle()
        {
            Context
                .Get<NameGenerator>()
                    .Select(t => t.GenerateBattleName())
                    .Save("BattleName")
                    .Continue()
                .Get<ApiClient>()
                    .Select(client => client.CreateBattle(new CreateBattleDTO()
                    {
                        Name = Context.Get<string>("BattleName").Value,
                        Description = ""
                    }))
                    .Save()
                    .Select(t => t.BattleId)
                    .Save("BattleId")
                    .Continue()
                .Get<ApiClient>()
                    .Select(t => t.GetBattle(Context.Get<Guid>("BattleId").Value))
                    .Save()
                    .Assert()
                        .NotNull()
                        .Equals(t => t.BattleId, Context.Get<Guid>("BattleId").Value)
                        .Equals(t => t.Name, Context.Get<string>("BattleName").Value);
        }
    }
}
