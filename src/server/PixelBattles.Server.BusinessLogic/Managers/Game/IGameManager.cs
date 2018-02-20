using PixelBattles.Server.BusinessLogic.Models;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public interface IGameManager : IDisposable
    {
        Task<Game> GetGameAsync(Guid gameId);
    }
}
