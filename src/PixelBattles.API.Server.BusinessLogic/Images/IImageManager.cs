using PixelBattles.API.Server.BusinessLogic.Images.Models;
using System;
using System.Threading.Tasks;

namespace PixelBattles.API.Server.BusinessLogic.Images
{
    public interface IImageManager
    {
        Task<Image> GetImageAsync(Guid imageId);
        Task<CreateImageResult> CreateImageAsync(CreateImageCommand command);
        Task<Result> UpdateImageAsync(UpdateImageCommand command);
        Task<Result> DeleteImageAsync(DeleteImageCommand command);
    }
}