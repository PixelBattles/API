using PixelBattles.API.Server.DataStorage.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.API.Server.DataStorage.Stores
{
    public interface IBattleStore
    {
        Task<BattleEntity> GetBattleAsync(Guid battleId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<BattleEntity>> GetBattlesAsync(BattleEntityFilter battleEntityFilter, CancellationToken cancellationToken = default(CancellationToken));
        Task<Result> UpdateBattleAsync(BattleEntity battleEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task<Result> CreateBattleAsync(BattleEntity battleEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task<Result> DeleteBattleAsync(Guid battleId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
