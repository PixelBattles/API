using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PixelBattles.API.Server.BusinessLogic.Images.Models;
using PixelBattles.API.Server.DataStorage.Stores.Images;
using System;
using System.Threading.Tasks;

namespace PixelBattles.API.Server.BusinessLogic.Images
{
    public class ImageManager : BaseManager, IImageManager
    {
        protected IImageStore ImageStore { get; set; }

        public ImageManager(
            IImageStore imageStore,
            IHttpContextAccessor contextAccessor,
            ErrorDescriber errorDescriber,
            IMapper mapper,
            ILogger<ImageManager> logger)
            : base(
                  contextAccessor: contextAccessor,
                  errorDescriber: errorDescriber,
                  mapper: mapper,
                  logger: logger)
        {
            ImageStore = imageStore ?? throw new ArgumentNullException(nameof(imageStore));
        }

        public async Task<Image> GetImageAsync(Guid imageId)
        {
            var image = await ImageStore.GetImageAsync(imageId, CancellationToken);
            return Mapper.Map<ImageEntity, Image>(image);
        }
        
        public async Task<CreateImageResult> CreateImageAsync(CreateImageCommand command)
        {
            if (String.IsNullOrWhiteSpace(command.Name))
            {
                return new CreateImageResult(new Error("Empty name", "Name can't be empty"));
            }

            var image = new ImageEntity()
            {
                Description = command.Description,
                Name = command.Name,
                Data = command.Data,
                ContentType = command.ContentType,
                FileName = command.FileName
            };

            var result = await ImageStore.CreateImageAsync(image, CancellationToken);
            if (result.Succeeded)
            {
                return new CreateImageResult(image.ImageId);
            }
            else
            {
                return new CreateImageResult(result.Errors);
            }
        }

        public async Task<Result> UpdateImageAsync(UpdateImageCommand command)
        {
            var image = await ImageStore.GetImageAsync(command.ImageId);
            if (image == null)
            {
                return Result.Failed(new Error("Image not found", "Image not found"));
            }
            image.Description = command.Description;
            image.Name = command.Name;
            image.Data = command.Data;
            image.ContentType = command.ContentType;
            image.FileName = command.FileName;
            return await ImageStore.UpdateImageAsync(image, CancellationToken);
        }

        public Task<Result> DeleteImageAsync(DeleteImageCommand command)
        {
            return ImageStore.DeleteImageAsync(command.ImageId, CancellationToken);
        }
    }
}