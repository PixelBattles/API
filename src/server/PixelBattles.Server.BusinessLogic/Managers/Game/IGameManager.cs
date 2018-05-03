using PixelBattles.Server.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public interface IGameManager
    {
        Task<Game> GetGameAsync(Guid gameId);

        Task<IEnumerable<Game>> GetBattleGamesAsync(Guid battleId);

        Task<Game> GetBattleGameAsync(Guid battleId);

        Task<CreateGameResult> CreateGameAsync(CreateGameCommand command);

        Task<CreateGameTokenResult> CreateGameTokenAsync(CreateGameTokenCommand command);
    }
}
