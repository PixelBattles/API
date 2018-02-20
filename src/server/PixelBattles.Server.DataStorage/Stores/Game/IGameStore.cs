using PixelBattles.Server.DataStorage.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.DataStorage.Stores
{
    public interface IGameStore : IStore<GameEntity>
    {
        Task<GameEntity> GetGameAsync(Guid gameId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
