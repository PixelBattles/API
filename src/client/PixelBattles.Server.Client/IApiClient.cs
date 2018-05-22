using PixelBattles.Shared.DataTransfer.Api.Battle;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.Client
{
    public interface IApiClient
    {
        Task<BattleDTO> GetBattleAsync(Guid battleId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
