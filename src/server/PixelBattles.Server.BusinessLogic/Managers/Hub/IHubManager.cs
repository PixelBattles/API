using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Server.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public interface IHubManager : IDisposable
    {
        Task<Hub> GetHubAsync(Guid hubId);

        Task<IEnumerable<Hub>> GetHubsAsync();

        Task<CreateHubResult> CreateHubAsync(CreateHubCommand command);

        Task<Result> DeleteHubAsync(Guid hubId);
    }
}
