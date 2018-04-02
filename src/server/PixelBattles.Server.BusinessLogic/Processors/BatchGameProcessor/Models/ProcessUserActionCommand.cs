using SixLabors.ImageSharp;
using System;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public class ProcessUserActionCommand
    {
        public Guid GameId { get; set; }

        public Guid UserId { get; set; }

        public int YIndex { get; set; }

        public int XIndex { get; set; }

        public Rgba32 Pixel { get; set; }
    }
}
