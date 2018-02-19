using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using System.IO;

namespace PixelBattles.Server.BusinessLogic.Tests
{
    public abstract class BaseGameProcessorTests
    {
        protected byte[] GetByteImageSample(int height = 1000, int width = 1000)
        {
            using (Image<Rgba32> image = new Image<Rgba32>(height, width))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        byte value = (byte)((y + x) % 255);
                        image[x, y] = new Rgba32(value, value, value, byte.MaxValue);
                    }
                }
                using (MemoryStream imageStream = new MemoryStream())
                {
                    PngEncoder pngEncoder = new PngEncoder
                    {
                        IgnoreMetadata = true,
                        CompressionLevel = 9,
                        PngColorType = PngColorType.RgbWithAlpha,
                    };
                    image.SaveAsPng(imageStream, pngEncoder);
                    return imageStream.ToArray();
                }
            }
        }

        protected Image<Rgba32> GetImageSample(int height = 1000, int width = 1000)
        {
            Rgba32[] pixels = new Rgba32[height * width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte value = (byte)((y + x) % 255);
                    pixels[y * x + x] = new Rgba32(value, value, value, byte.MaxValue);
                }
            }
            return Image.LoadPixelData(pixels, width, height);
        }
    }
}
