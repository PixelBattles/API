using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.API.Server.DataStorage.Stores.Images
{
    public class ImageStore : IImageStore
    {
        private readonly IMongoCollection<ImageEntity> _imageCollection;

        public ImageStore(IMongoCollection<ImageEntity> imageCollection)
        {
            _imageCollection = imageCollection ?? throw new ArgumentNullException(nameof(imageCollection));
        }

        public async Task<ImageEntity> GetImageAsync(Guid imageId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _imageCollection.FindAsync(t => t.ImageId == imageId, null, cancellationToken);
            return await result.SingleAsync(cancellationToken);
        }

        public async Task<Result> CreateImageAsync(ImageEntity imageEntity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _imageCollection.InsertOneAsync(imageEntity, null, cancellationToken);
            return Result.Success;
        }

        public async Task<Result> UpdateImageAsync(ImageEntity imageEntity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _imageCollection.ReplaceOneAsync(t => t.ImageId == imageEntity.ImageId, imageEntity, new UpdateOptions { IsUpsert = false }, cancellationToken);
            if (result.MatchedCount == 0)
            {
                return Result.Failed(new Error("Image not found", "Image not found"));
            }
            return Result.Success;
        }

        public async Task<Result> DeleteImageAsync(Guid imageId, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _imageCollection.DeleteOneAsync(t => t.ImageId == imageId, cancellationToken);
            return Result.Success;
        }
    }
}
