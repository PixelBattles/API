using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.DataStorage.Models;
using System;
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
    }
}
