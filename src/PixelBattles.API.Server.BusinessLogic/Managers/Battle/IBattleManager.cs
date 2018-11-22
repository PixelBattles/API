using PixelBattles.API.Server.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelBattles.API.Server.BusinessLogic.Managers
{
    public interface IBattleManager
    {
        Task<Battle> GetBattleAsync(Guid battleId);
        Task<IEnumerable<Battle>> GetBattlesAsync(BattleFilter battleFilter);
        Task<CreateBattleResult> CreateBattleAsync(CreateBattleCommand command);
        Task<Result> UpdateBattleAsync(UpdateBattleCommand command);
        Task<Result> DeleteBattleAsync(DeleteBattleCommand command);
        Task<CreateBattleTokenResult> CreateBattleTokenAsync(CreateBattleTokenCommand command);
    }
}
