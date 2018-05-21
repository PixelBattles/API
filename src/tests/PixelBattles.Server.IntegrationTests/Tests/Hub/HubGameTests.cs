using PixelBattles.Fluently;
using PixelBattles.Server.IntegrationTests.Clients;
using PixelBattles.Server.IntegrationTests.Clients.Api;
using PixelBattles.Server.IntegrationTests.Clients.Hub;
using PixelBattles.Shared.DataTransfer.Api.Battle;
using PixelBattles.Shared.DataTransfer.Api.Game;
using System;
using Xunit;

namespace PixelBattles.Server.IntegrationTests.Tests
{
    public class HubGameTests : FlowTest
    {
        public HubGameTests()
        {
            Context
                .Setup(new ApiClient())
                    .With(client => client.BackendUrl = Configuration.GetApiBaseUrl())
                    .Save()
                    .Continue()
                .Setup(new NameGenerator())
                    .Save()
                    .Continue()
                .Get<ApiClient>()
                    .Select(client => client.CreateBattle(new CreateBattleDTO()
                    {
                        Name = Context.Get<NameGenerator>().Value.GenerateBattleName(),
                        Description = ""
                    }))
                    .Select(t => t.BattleId)
                    .Save("BattleId")
                    .Continue()
                .Get<ApiClient>()
                    .Select(client => client.CreateGame(new CreateGameDTO()
                    {
                        BattleId = Context.Get<Guid>("BattleId").Value,
                        Cooldown = 60,
                        StartDateUTC = DateTime.UtcNow,
                        EndDateUTC = DateTime.UtcNow.AddDays(1),
                        Height = 100,
                        Width = 100,
                        Name = Context.Get<NameGenerator>().Value.GenerateGameName()
                    }))
                    .Select(t => t.GameId)
                    .Save("GameId")
                    .Continue()
                .Get<ApiClient>()
                    .Select(client => client.GetGameToken(new CreateGameTokenDTO()
                    {
                        GameId = Context.Get<Guid>("GameId").Value
                    }))
                    .Select(t => t.Token)
                    .Save("Token")
                    .Continue();
        }

        [Fact]
        public void HubClient_Can_Connect_Hub()
        {
            Context
                .Setup(new HubClient())
                    .With(t => t.BackendUrl = Configuration.GetHubBaseUrl())
                    .With(t => t.Token = Context.Get<string>("Token").Value)
                    .Save()
                    .With(t => t.Connect())
                    .Continue();
        }

        [Fact]
        public void HubClient_Can_Get_GameInfo()
        {
            Context
                .Setup(new HubClient())
                    .With(t => t.BackendUrl = Configuration.GetHubBaseUrl())
                    .With(t => t.Token = Context.Get<string>("Token").Value)
                    .Save()
                    .With(t => t.Connect())
                    .Select(t => t.GetGameInfo())
                    .Save()
                    .Assert()
                        .NotNull()
                        .Equals(t => t.GameId, Context.Get<Guid>("GameId").Value)
                    .Continue();
        }
    }
}
