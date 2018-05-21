using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Server.Core;
using PixelBattles.Server.DataStorage.Models;
using PixelBattles.Server.DataStorage.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    [Register(typeof(IGameManager))]
    public class GameManager : BaseManager, IGameManager
    {
        protected IGameStore GameStore { get; set; }
        protected IGameTokenGenerator GameTokenGenerator { get; set; }

        public GameManager(
            IGameStore gameStore,
            IGameTokenGenerator gameTokenGenerator,
            IHttpContextAccessor contextAccessor,
            ErrorDescriber errorDescriber,
            IMapper mapper,
            ILogger<GameManager> logger)
            : base(
                  contextAccessor: contextAccessor,
                  errorDescriber: errorDescriber,
                  mapper: mapper,
                  logger: logger)
        {
            GameStore = gameStore ?? throw new ArgumentNullException(nameof(gameStore));
            GameTokenGenerator = gameTokenGenerator ?? throw new ArgumentNullException(nameof(gameTokenGenerator));
        }
        
        public async Task<Game> GetGameAsync(Guid gameId)
        {
            var game = await GameStore.GetGameAsync(gameId, CancellationToken);
            return Mapper.Map<GameEntity, Game>(game);
        }

        public async Task<IEnumerable<Game>> GetBattleGamesAsync(Guid battleId)
        {
            var games = await GameStore.GetBattleGamesAsync(battleId, CancellationToken);
            return Mapper.Map<IEnumerable<GameEntity>, IEnumerable<Game>>(games);
        }

        public async Task<Game> GetBattleGameAsync(Guid battleId)
        {
            var game = await GameStore.GetBattleGameAsync(battleId, CancellationToken);
            return Mapper.Map<GameEntity, Game>(game);
        }

        public async Task<CreateGameResult> CreateGameAsync(CreateGameCommand command)
        {
            if (command.StartDateUTC >= command.EndDateUTC)
            {
                return new CreateGameResult(new Error("Wrong time limits", "Start date can't be later than end date"));
            }

            var lastGame = await GameStore.GetBattleGameAsync(command.BattleId, CancellationToken);
            if (lastGame != null)
            {
                if (command.StartDateUTC <= lastGame.EndDateUTC)
                {
                    return new CreateGameResult(new Error("Wrong time limits", "Start date can't be earlier than last game end date"));
                }
            }

            var game = new GameEntity()
            {
                ChangeIndex = 0,
                BattleId = command.BattleId,
                Height = command.Height,
                Width = command.Width,
                Cooldown = command.Cooldown,
                StartDateUTC = command.StartDateUTC.Value,
                EndDateUTC = command.EndDateUTC.Value,
                State = null,
                Name = command.Name
            };
            
            var result = await GameStore.CreateAsync(game, CancellationToken);

            if (result.Succeeded)
            {
                return new CreateGameResult(game.GameId);
            }
            else
            {
                return new CreateGameResult(result.Errors);
            }
        }

        public async Task<CreateGameTokenResult> CreateGameTokenAsync(CreateGameTokenCommand command)
        {            
            var game = await GameStore.GetGameAsync(command.GameId, CancellationToken);

            if (game == null)
            {
                return new CreateGameTokenResult(new Error("Game not found", "Game not found"));
            }

            string token = GameTokenGenerator.GenerateToken(command.GameId, command.UserId);

            return new CreateGameTokenResult(token);
        }
    }
}
