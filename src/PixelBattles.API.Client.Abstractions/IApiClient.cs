using PixelBattles.API.DataTransfer;
using PixelBattles.API.DataTransfer.Battles;
using PixelBattles.API.DataTransfer.Images;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.API.Client
{
    public interface IApiClient
    {
        Task<BattleDTO> GetBattleAsync(long battleId, CancellationToken cancellationToken = default);
        Task<ResultDTO> UpdateBattleImageAsync(long battleId, UpdateBattleImageDTO updateBattleImage, CancellationToken cancellationToken = default);
        Task<CreateImageResultDTO> CreateImageAsync(CreateImageDTO createImage, byte[] data, CancellationToken cancellationToken = default);
        Task<ResultDTO> UpdateImageAsync(Guid imageId, UpdateImageDTO updateImage, byte[] data, CancellationToken cancellationToken = default);
    }
}