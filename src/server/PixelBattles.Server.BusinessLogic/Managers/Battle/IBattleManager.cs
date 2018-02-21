using PixelBattles.Server.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public interface IBattleManager : IDisposable
    {
        Task<Battle> GetBattleAsync(Guid battleId);

        Task<IEnumerable<Battle>> GetBattlesAsync(BattleFilter battleFilter);

        Task<CreateBattleResult> CreateBattleAsync(CreateBattleCommand command);
    }
}
