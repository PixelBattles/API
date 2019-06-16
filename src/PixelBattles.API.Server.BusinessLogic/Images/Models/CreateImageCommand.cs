using System;

namespace PixelBattles.API.Server.BusinessLogic.Images.Models
{
    public class CreateImageCommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Data { get; set; }
    }
}