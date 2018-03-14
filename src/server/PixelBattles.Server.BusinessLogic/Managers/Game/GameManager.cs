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

        public GameManager(
            IGameStore gameStore,
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
        }

        protected override void DisposeStores()
        {
            GameStore.Dispose();
        }
        
        public async Task<Game> GetGameAsync(Guid gameId)
        {
            ThrowIfDisposed();
            var game = await GameStore.GetGameAsync(gameId, CancellationToken);
            return Mapper.Map<GameEntity, Game>(game);
        }

        public async Task<IEnumerable<Game>> GetBattleGamesAsync(Guid battleId)
        {
            ThrowIfDisposed();
            var games = await GameStore.GetBattleGamesAsync(battleId, CancellationToken);
            return Mapper.Map<IEnumerable<GameEntity>, IEnumerable<Game>>(games);
        }

        public async Task<Game> GetBattleGameAsync(Guid battleId)
        {
            ThrowIfDisposed();
            var game = await GameStore.GetBattleGameAsync(battleId, CancellationToken);
            return Mapper.Map<GameEntity, Game>(game);
        }

        public async Task<CreateGameResult> CreateGameAsync(CreateGameCommand command)
        {
            ThrowIfDisposed();

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
                StartDateUTC = command.StartDateUTC,
                EndDateUTC = command.EndDateUTC,
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
    }
}
