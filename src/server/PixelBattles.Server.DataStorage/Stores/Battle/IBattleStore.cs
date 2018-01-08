using PixelBattles.Server.DataStorage.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.DataStorage.Stores
{
    public interface IBattleStore : IStore<BattleEntity>
    {
        Task<BattleEntity> GetBattleAsync(Guid battleId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
