using PixelBattles.API.DataTransfer;
using PixelBattles.API.DataTransfer.Battles;
using PixelBattles.API.DataTransfer.Images;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.API.Client
{
    public interface IApiClient : IDisposable
    {
        Task<BattleDTO> GetBattleAsync(long battleId, CancellationToken cancellationToken = default);
        Task<ResultDTO> UpdateBattleImageAsync(long battleId, UpdateBattleImageDTO updateBattleImage, CancellationToken cancellationToken = default);
        Task<CreateImageResultDTO> CreateImageAsync(CreateImageDTO createImage, byte[] data, string fileName, string contentType, CancellationToken cancellationToken = default);
        Task<ResultDTO> UpdateImageAsync(Guid imageId, UpdateImageDTO updateImage, byte[] data, string fileName, string contentType, CancellationToken cancellationToken = default);
    }
}