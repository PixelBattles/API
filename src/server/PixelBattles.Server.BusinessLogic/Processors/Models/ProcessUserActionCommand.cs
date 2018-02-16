using SixLabors.ImageSharp;
using System;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public class ProcessUserActionCommand
    {
        public Guid GameId { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public Rgba32 Pixel { get; set; }
    }
}
