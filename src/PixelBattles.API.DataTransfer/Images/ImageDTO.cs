using System;

namespace PixelBattles.API.DataTransfer.Images
{
    public class ImageDTO
    {
        public Guid ImageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}