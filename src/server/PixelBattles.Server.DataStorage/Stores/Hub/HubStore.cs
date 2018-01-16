using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.DataStorage.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.DataStorage.Stores
{
    [Register(typeof(IHubStore))]
    public class HubStore : BaseStore<HubEntity>, IHubStore
    {
        public HubStore(
            PixelBattlesDbContext context) : base(
                context: context)
        {

        }

        public async Task<HubEntity> GetHubAsync(Guid hubId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var battle = await Entities
                .FirstOrDefaultAsync(t => t.HubId == hubId, cancellationToken);
            return battle;
        }
    }
}
