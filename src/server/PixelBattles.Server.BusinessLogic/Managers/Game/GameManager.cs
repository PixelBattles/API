using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Server.DataStorage.Models;
using PixelBattles.Server.DataStorage.Stores;
using System;
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
    }
}
