using PixelBattles.Server.Core;
using PixelBattles.Server.DataStorage.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.DataStorage.Stores
{
    public interface IUserActionStore : IStore<UserActionEntity>
    {
        Task<Result> CreateBatchAsync(IEnumerable<UserActionEntity> userActions, CancellationToken cancellationToken = default(CancellationToken));
    }
}
