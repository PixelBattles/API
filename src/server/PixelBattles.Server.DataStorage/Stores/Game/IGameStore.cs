using PixelBattles.Server.DataStorage.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.DataStorage.Stores
{
    public interface IGameStore : IStore<GameEntity>
    {
        Task<GameEntity> GetGameAsync(Guid gameId, CancellationToken cancellationToken = default(CancellationToken));
        
        Task<IEnumerable<GameEntity>> GetBattleGamesAsync(Guid battleId, CancellationToken cancellationToken = default(CancellationToken));

        Task<GameEntity> GetBattleGameAsync(Guid battleId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
