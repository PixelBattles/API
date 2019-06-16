using System;

namespace PixelBattles.API.Server.BusinessLogic.Images.Models
{
    public class UpdateImageCommand
    {
        public Guid ImageId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Data { get; set; }
    }
}