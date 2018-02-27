using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.DataStorage.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.DataStorage.Stores
{
    [Register(typeof(IUserBattleStore))]
    public class UserBattleStore : BaseStore<UserBattleEntity>, IUserBattleStore
    {
        public UserBattleStore(
            PixelBattlesDbContext context) : base(
                context: context)
        {

        }

        public async Task<UserBattleEntity> GetUserBattleAsync(Guid userId, Guid battleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var battle = await Entities
                .FirstOrDefaultAsync(t => t.BattleId == battleId, cancellationToken);
            return battle;
        }

        public async Task<UserBattleEntity> GetUserBattleFromGameAsync(Guid userId, Guid gameId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var game = await Context.Set<GameEntity>().FirstOrDefaultAsync(t => t.GameId == gameId);
            if (game == null)
            {
                return null;
            }

            var battle = await Entities
                .FirstOrDefaultAsync(t => t.BattleId == game.BattleId, cancellationToken);

            return battle;
        }
    }
}
