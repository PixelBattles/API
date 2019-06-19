using System;

namespace PixelBattles.API.DataTransfer.Images
{
    public class CreateImageResultDTO : ResultDTO
    {
        public Guid? ImageId { get; set; }
    }
}