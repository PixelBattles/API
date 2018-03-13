using PixelBattles.Server.DataStorage.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.DataStorage.Stores
{
    public interface IBattleStore : IStore<BattleEntity>
    {
        Task<BattleEntity> GetBattleAsync(Guid battleId, CancellationToken cancellationToken = default(CancellationToken));

        Task<BattleEntity> GetBattleAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<BattleEntity>> GetBattlesAsync(BattleEntityFilter battleFilter, CancellationToken cancellationToken = default(CancellationToken));
    }
}
