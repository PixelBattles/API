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
    [Register(typeof(IBattleStore))]
    public class BattleStore : BaseStore<BattleEntity>, IBattleStore
    {
        public BattleStore(
            PixelBattlesDbContext context) : base(
                context: context)
        {

        }

        public async Task<BattleEntity> GetBattleAsync(Guid battleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var battle = await Entities
                .FirstOrDefaultAsync(t => t.BattleId == battleId, cancellationToken);

            return battle;
        }

        public async Task<IEnumerable<BattleEntity>> GetBattlesAsync(BattleEntityFilter battleFilter, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var battlesQuery = ApplyFilter(Entities, battleFilter);

            var battles = await battlesQuery
                .ToListAsync(cancellationToken);

            return battles;
        }

        private IQueryable<BattleEntity> ApplyFilter(IQueryable<BattleEntity> battles, BattleEntityFilter filter)
        {
            if (filter.UserId.HasValue)
            {
                var userId = filter.UserId;
                battles = battles.Where(t => t.UserBattles.Any(ub => ub.UserId == userId));
            }
            if (!String.IsNullOrWhiteSpace(filter.Name))
            {
                var name = filter.Name;
                battles = battles.Where(t => t.Name.Contains(name));
            }
            return battles;
        }
    }
}
