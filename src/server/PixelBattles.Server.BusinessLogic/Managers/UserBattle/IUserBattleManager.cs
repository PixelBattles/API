using PixelBattles.Server.BusinessLogic.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public interface IUserBattleManager : IDisposable
    {
        Task<UserBattle> GetUserBattleAsync(Guid userId, Guid battleId, CancellationToken cancellationToken = default(CancellationToken));

        Task<UserBattle> GetUserBattleFromGameAsync(Guid userId, Guid gameId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
