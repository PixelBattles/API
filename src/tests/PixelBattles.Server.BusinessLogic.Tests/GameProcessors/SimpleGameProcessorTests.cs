using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Server.BusinessLogic.Processors;
using SixLabors.ImageSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PixelBattles.Server.BusinessLogic.Tests
{
    public class SimpleGameProcessorTests : BaseGameProcessorTests
    {
        [Fact]
        public void SimpleGameProcessor_CanBeCreated_FromEmptyGame()
        {
            Game game = new Game()
            {
                ChangeIndex = 1,
                GameId = Guid.NewGuid(),
                Height = 1000,
                Width = 1000,
                State = GetByteImageSample(1000, 1000)
            };

            IGameProcessor gameProcessor = new BatchGameProcessor(game);

            Assert.NotNull(gameProcessor);
        }

        [Fact]
        public async Task SimpleGameProcessor_ReturnValidState_FromEmpyState()
        {
            Game game = new Game()
            {
                ChangeIndex = 1,
                GameId = Guid.NewGuid(),
                Height = 1000,
                Width = 1000,
                State = GetByteImageSample(1000, 1000)
            };

            IGameProcessor gameProcessor = new BatchGameProcessor(game);

            var gameState = await gameProcessor.GetGameStateAsync();

            Assert.NotNull(gameState);
            Assert.NotNull(gameState.PendingActions);
            Assert.NotNull(gameState.State);

            Assert.Empty(gameState.PendingActions);
        }

        [Fact]
        public void SimpleGameProcessor_CanBeCreated_FromNonEmptyGame()
        {
            Game game = new Game()
            {
                ChangeIndex = 1,
                GameId = Guid.NewGuid(),
                Height = 1000,
                Width = 1000,
                State = GetByteImageSample(1000, 1000)
            };
            
            IGameProcessor gameProcessor = new BatchGameProcessor(game);

            Assert.NotNull(gameProcessor);
        }

        [Fact]
        public async Task SimpleGameProcessor_ReturnValidState_FromNonEmpyState()
        {
            Game game = new Game()
            {
                ChangeIndex = 1,
                GameId = Guid.NewGuid(),
                Height = 1000,
                Width = 1000,
                State = GetByteImageSample(1000, 1000)
            };

            IGameProcessor gameProcessor = new BatchGameProcessor(game);

            var gameState = await gameProcessor.GetGameStateAsync();

            Assert.NotNull(gameState);
            Assert.NotNull(gameState.PendingActions);
            Assert.NotNull(gameState.State);

            Assert.True(gameState.State.SequenceEqual(game.State));
            Assert.Empty(gameState.PendingActions);
        }

        [Fact]
        public async Task SimpleGameProcessor_CanHandle_UserAction()
        {
            Game game = new Game()
            {
                ChangeIndex = 1,
                GameId = Guid.NewGuid(),
                Height = 1000,
                Width = 1000,
                State = GetByteImageSample(1000, 1000)
            };

            ProcessUserActionCommand command = new ProcessUserActionCommand()
            {
                GameId = game.GameId,
                XIndex = 0,
                YIndex = 0,
                Pixel = new Rgba32(255, 255, 255, byte.MaxValue)
            };

            IGameProcessor gameProcessor = new BatchGameProcessor(game);

            var result = await gameProcessor.ProcessUserActionAsync(command);

            Assert.NotNull(result);
            Assert.True(result.Succeeded);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task SimpleGameProcessor_HandleUserAction_ReturnProperState()
        {
            Game game = new Game()
            {
                ChangeIndex = 1,
                GameId = Guid.NewGuid(),
                Height = 1000,
                Width = 1000,
                State = GetByteImageSample(1000, 1000)
            };

            ProcessUserActionCommand command = new ProcessUserActionCommand()
            {
                GameId = game.GameId,
                XIndex = 0,
                YIndex = 0,
                Pixel = new Rgba32(255, 255, 255, byte.MaxValue)
            };

            IGameProcessor gameProcessor = new BatchGameProcessor(game);

            var result = await gameProcessor.ProcessUserActionAsync(command);

            var state = await gameProcessor.GetGameStateAsync();

            Assert.Single(state.PendingActions);

            var action = state.PendingActions.Single();
            Assert.Equal(action.Key, game.ChangeIndex + 1);
            Assert.Equal(action.Value.ChangeIndex, game.ChangeIndex + 1);
            Assert.Equal(action.Value.XIndex, command.XIndex);
            Assert.Equal(action.Value.YIndex, command.YIndex);
            Assert.Equal(action.Value.Pixel, command.Pixel);
        }
    }
}
