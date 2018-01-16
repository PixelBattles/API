using PixelBattles.Server.BusinessLogic.Models;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public interface IHubManager : IDisposable
    {
        Task<Hub> GetHubAsync(Guid hubId);
    }
}
