using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PixelBattles.API.DataTransfer;
using PixelBattles.API.DataTransfer.Images;
using PixelBattles.API.Server.BusinessLogic.Images;
using PixelBattles.API.Server.BusinessLogic.Images.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PixelBattles.API.Server.Web.Controllers.Api
{
    [Route("api")]
    public class ImageController : BaseApiController
    {
        protected IImageManager ImageManager { get; set; }

        public ImageController(
            IImageManager imageManager,
            IMapper mapper,
            ILogger<ImageController> logger) : base(
                mapper: mapper,
                logger: logger)
        {
            ImageManager = imageManager ?? throw new ArgumentNullException(nameof(imageManager));
        }

        [HttpGet("image/{imageId:guid}")]
        public async Task<IActionResult> GetImageAsync(Guid imageId)
        {
            try
            {
                var image = await ImageManager.GetImageAsync(imageId);
                if (image == null)
                {
                    return NotFound();
                }
                var result = Mapper.Map<Image, ImageDTO>(image);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while getting image.");
            }
        }

        [HttpGet("image/{imageId:guid}/content")]
        public async Task<IActionResult> GetImageContentAsync(Guid imageId)
        {
            try
            {
                var image = await ImageManager.GetImageAsync(imageId);
                if (image == null)
                {
                    return NotFound();
                }
                return File(image.Data, image.ContentType);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while getting image content.");
            }
        }

        [HttpPost("image")]
        public async Task<IActionResult> CreateImageAsync(CreateImageDTO commandDTO, IFormFile file)
        {
            try
            {
                using (var fileStream = new MemoryStream())
                {
                    await file.CopyToAsync(fileStream);
                    var command = new CreateImageCommand()
                    {
                        Name = commandDTO.Name,
                        Description = commandDTO.Description,
                        Data = fileStream.ToArray(),
                        ContentType = file.ContentType,
                        FileName = file.FileName
                    };
                    var result = await ImageManager.CreateImageAsync(command);
                    var resultDTO = Mapper.Map<CreateImageResult, CreateImageResultDTO>(result);
                    return OnResult(resultDTO);
                }
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while creating image.");
            }
        }

        [HttpDelete("image/{imageId:guid}")]
        public async Task<IActionResult> DeleteImageAsync(Guid imageId)
        {
            try
            {
                var command = new DeleteImageCommand()
                {
                    ImageId = imageId
                };
                var result = await ImageManager.DeleteImageAsync(command);
                var resultDTO = Mapper.Map<Result, ResultDTO>(result);
                return OnResult(resultDTO);
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while deleting image.");
            }
        }

        [HttpPut("image/{imageId:guid}")]
        public async Task<IActionResult> UpdateImageAsync(Guid imageId, UpdateImageDTO commandDTO, IFormFile file)
        {
            try
            {
                using (var fileStream = new MemoryStream())
                {
                    await file.CopyToAsync(fileStream);
                    var command = new UpdateImageCommand()
                    {
                        ImageId = imageId,
                        Name = commandDTO.Name,
                        Description = commandDTO.Description,
                        ContentType = file.ContentType,
                        FileName = file.FileName,
                        Data = fileStream.ToArray()
                    };
                    var result = await ImageManager.UpdateImageAsync(command);
                    var resultDTO = Mapper.Map<Result, ResultDTO>(result);
                    return OnResult(resultDTO);
                }
            }
            catch (Exception exception)
            {
                return OnException(exception, "Error while updating image.");
            }
        }
    }
}