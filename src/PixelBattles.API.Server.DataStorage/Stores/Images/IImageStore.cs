using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.API.Server.DataStorage.Stores.Images
{
    public interface IImageStore
    {
        Task<ImageEntity> GetImageAsync(Guid imageId, CancellationToken cancellationToken = default(CancellationToken));
        Task<Result> UpdateImageAsync(ImageEntity battleEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task<Result> CreateImageAsync(ImageEntity battleEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task<Result> DeleteImageAsync(Guid imageId, CancellationToken cancellationToken = default(CancellationToken));
    }
}