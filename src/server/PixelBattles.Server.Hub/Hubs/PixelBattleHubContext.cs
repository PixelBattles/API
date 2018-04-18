using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.BusinessLogic.Managers;
using PixelBattles.Shared.DataTransfer.Hub;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace PixelBattles.Server.Hubs
{
    public class PixelBattleHubContext
    {
        protected ConcurrentDictionary<Guid, GameInfoDTO> Games { get; set; }

        protected IHubContext<GameHub> GameHubContext { get; set; }

        protected IServiceScopeFactory ServiceScopeFactory { get; set; }

        public PixelBattleHubContext(
            IServiceScopeFactory serviceScopeFactory,
            IHubContext<GameHub> gameHubContext)
        {
            this.GameHubContext = gameHubContext;
            this.ServiceScopeFactory = serviceScopeFactory;
            this.Games = new ConcurrentDictionary<Guid, GameInfoDTO>();
        }

        public async Task<GameInfoDTO> GetGameAsync(Guid gameId)
        {
            if (Games.TryGetValue(gameId, out GameInfoDTO gameInfo))
            {
                return gameInfo;
            }
            else
            {
                using (var scope = ServiceScopeFactory.CreateScope())
                {
                    var gameManager = scope.ServiceProvider.GetRequiredService<IGameManager>();
                    var game = await gameManager.GetGameAsync(gameId);
                    gameInfo = new GameInfoDTO()
                    {
                        GameId = gameId,
                        GameSizeX = game.Width,
                        GameSizeY = game.Height,
                        ChunkSizeX = game.Width,
                        ChunkSizeY = game.Height
                    };
                }
                if (gameInfo != null)
                {
                    gameInfo = Games.AddOrUpdate(gameId, gameInfo, (k, v) => v);
                }
            }
            return gameInfo;
        }
    }
}
