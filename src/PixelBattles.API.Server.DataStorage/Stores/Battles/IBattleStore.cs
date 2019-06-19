using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.API.Server.DataStorage.Stores.Battles
{
    public interface IBattleStore
    {
        Task<BattleEntity> GetBattleAsync(long battleId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<BattleEntity>> GetBattlesAsync(BattleEntityFilter battleEntityFilter, CancellationToken cancellationToken = default(CancellationToken));
        Task<Result> UpdateBattleAsync(BattleEntity battleEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task<Result> CreateBattleAsync(BattleEntity battleEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task<Result> DeleteBattleAsync(long battleId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
