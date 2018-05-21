using PixelBattles.Fluently;
using PixelBattles.Server.IntegrationTests.Clients;
using PixelBattles.Server.IntegrationTests.Clients.Api;
using PixelBattles.Shared.DataTransfer.Api.Battle;
using PixelBattles.Shared.DataTransfer.Api.Game;
using System;
using Xunit;

namespace PixelBattles.Server.IntegrationTests.Tests
{
    public class ApiGameTests : FlowTest
    {
        public ApiGameTests()
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
                    .Save()
                    .Select(t => t.BattleId)
                    .Save("BattleId")
                    .Continue();
        }
        
        [Fact]
        public void ApiClient_Can_Create_Battle_Game()
        {
            Context
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
                    .Save()
                    .Assert()
                        .True(t => t.Succeeded)
                        .Empty(t => t.Errors)
                        .NotNull(t => t.GameId);
        }
        [Fact]
        public void ApiClient_Can_Get_Game_Token()
        {
            Context
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
                    .Save()
                    .Assert()
                        .NotNull()
                        .Empty(t => t.Errors)
                        .True(t => t.Succeeded)
                        .NotNull(t => t.Token);
        }
    }
}
