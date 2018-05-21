using PixelBattles.Shared.DataTransfer.Api.Game;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.Client
{
    public interface IApiClient
    {
        Task<GameDTO> GetGameAsync(Guid gameId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
