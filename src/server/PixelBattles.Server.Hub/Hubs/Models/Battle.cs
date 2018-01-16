using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;

namespace PixelBattles.Server.Hub.Models
{
    public class Battle
    {
        private object lockObject;

        public int Height { get; protected set; }

        public int Width { get; protected set; }

        protected Rgb24[] Pixels { get; set; }
        
        public int ChangeIndex { get; protected set; }

        public Battle()
        {
            this.ChangeIndex = 0;
            this.Height = 1000;
            this.Width = 1000;
            this.Pixels = new Rgb24[Height * Width];
        }

        public int SetPixel(Rgb24 color, int height, int width)
        {
            int newIndex;

            lock (lockObject)
            {
                this.Pixels[height * width] = color;
                newIndex = ChangeIndex++;
            }

            return newIndex;
        }

        public Stream GetImage()
        {
            Stream stream = null;
            var image = Image.LoadPixelData(this.Pixels, this.Width, this.Height);
            PngEncoder pngEncoder = new PngEncoder
            {
                IgnoreMetadata = true,
                PngColorType = PngColorType.Rgb
            };
            image.SaveAsPng(stream, pngEncoder);
            return stream;
        }
    }
}
