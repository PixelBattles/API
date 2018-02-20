using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.DataStorage.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.DataStorage.Stores
{
    [Register(typeof(IGameStore))]
    public class GameStore : BaseStore<GameEntity>, IGameStore
    {
        public GameStore(
            PixelBattlesDbContext context) : base(
                context: context)
        {

        }

        public async Task<GameEntity> GetGameAsync(Guid gameId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var game = await Entities
                .FirstOrDefaultAsync(t => t.GameId == gameId, cancellationToken);
            return game;
        }
    }
}
