using PixelBattles.API.DataTransfer.Battle;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.API.Client
{
    public interface IApiClient
    {
        Task<BattleDTO> GetBattleAsync(Guid battleId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
