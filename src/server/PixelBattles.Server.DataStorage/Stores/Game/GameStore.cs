using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.DataStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<GameEntity> GetBattleGameAsync(Guid battleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var game = await Entities
                .Where(t => t.BattleId == battleId)
                .OrderBy(t => t.StartDateUTC)
                .LastOrDefaultAsync(cancellationToken);
            return game;
        }

        public async Task<IEnumerable<GameEntity>> GetBattleGamesAsync(Guid battleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var games = await Entities
                .Where(t => t.BattleId == battleId)
                .ToListAsync(cancellationToken);
            return games;
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
