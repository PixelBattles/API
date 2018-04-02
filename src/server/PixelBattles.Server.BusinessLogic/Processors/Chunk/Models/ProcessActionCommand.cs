using SixLabors.ImageSharp;

namespace PixelBattles.Server.BusinessLogic.Processors.Chunk.Models
{
    public class ProcessActionCommand
    {
        public int XIndex { get; set; }

        public int YIndex { get; set; }
        
        public Rgba32 Pixel { get; set; }
    }
}
