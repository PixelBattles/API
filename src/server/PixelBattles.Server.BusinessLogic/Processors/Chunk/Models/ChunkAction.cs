using SixLabors.ImageSharp;

namespace PixelBattles.Server.BusinessLogic.Processors.Chunk.Models
{
    public class ChunkAction
    {
        public int ChangeIndex { get; set; }

        public int YIndex { get; set; }

        public int XIndex { get; set; }

        public Rgba32 Pixel { get; set; }
    }
}
