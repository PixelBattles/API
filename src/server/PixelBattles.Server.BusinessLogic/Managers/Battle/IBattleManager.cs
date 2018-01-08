using PixelBattles.Server.BusinessLogic.Models;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public interface IBattleManager : IDisposable
    {
        Task<Battle> GetBattleAsync(Guid battleId);
    }
}
