using PixelBattles.Server.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public interface IGameManager : IDisposable
    {
        Task<Game> GetGameAsync(Guid gameId);

        Task<IEnumerable<Game>> GetBattleGamesAsync(Guid battleId);

        Task<CreateBattleResult> CreateBattleAsync(CreateBattleCommand command);
    }
}
