using PixelBattles.Server.DataStorage.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.DataStorage.Stores
{
    public interface IUserBattleStore : IStore<UserBattleEntity>
    {
        Task<UserBattleEntity> GetUserBattleAsync(Guid userId, Guid battleId, CancellationToken cancellationToken = default(CancellationToken));

        Task<UserBattleEntity> GetUserBattleFromGameAsync(Guid userId, Guid gameId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
