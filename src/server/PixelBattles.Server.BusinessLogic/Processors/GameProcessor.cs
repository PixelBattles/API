using PixelBattles.Server.BusinessLogic.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public abstract class GameProcessor : IDisposable
    {
        bool disposed = false;

        protected Rgba32[] Pixels { get; set; }

        protected byte[] State;

        protected int ChangeIndex;
        
        protected Game Game { get; set; }
        
        protected ConcurrentQueue<UserAction> ActionQueue { get; set; }
        
        public GameProcessor(Game game)
        {
            Game = game ?? throw new ArgumentNullException(nameof(game));
            ActionQueue = new ConcurrentQueue<UserAction>();
            
            ChangeIndex = Game.ChangeIndex ?? default(int);
            if (Game.State == null)
            {
                Pixels = new Rgba32[Game.Height * Game.Width];
            }
            else
            {
                Pixels = GetPixelsFromBytes(game.State);
            }
            UpdateState();
        }
        
        protected void UpdateState()
        {
            var items = ActionQueue.ToArray();
            
            foreach (var item in items)
            {
                item.ChangeIndex = Interlocked.Increment(ref ChangeIndex);
                Pixels[item.YIndex * Game.Width + item.XIndex] = item.Pixel;
            }

            byte[] newState = GetBytesFromPixels(Pixels);

            var oldState = Interlocked.Exchange(ref State, newState);

            foreach (var item in items)
            {
                ActionQueue.TryDequeue(out UserAction tempUserAction);
            }
        }
        
        protected Rgba32[] GetPixelsFromBytes(byte[] imageArray)
        {
            Rgba32[] tempPixels = new Rgba32[Game.Height * Game.Width];
            IImageDecoder imageDecoder = new PngDecoder()
            {
                IgnoreMetadata = true
            };

            var image = Image.Load(imageArray, imageDecoder);
            for (int y = 0; y < Game.Height; y++)
            {
                for (int x = 0; x < Game.Width; x++)
                {
                    tempPixels[y * Game.Width + x] = image[x, y];
                }
            }
            return tempPixels;
        }

        protected byte[] GetBytesFromPixels(Rgba32[] pixelArray)
        {
            byte[] byteArray;
            using (MemoryStream stream = new MemoryStream())
            {
                var image = Image.LoadPixelData(pixelArray, Game.Width, Game.Height);
                PngEncoder pngEncoder = new PngEncoder
                {
                    IgnoreMetadata = true,
                    CompressionLevel = 9,
                    PngColorType = PngColorType.RgbWithAlpha
                };
                image.SaveAsPng(stream, pngEncoder);
                byteArray = stream.ToArray();
            }
            return byteArray;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                //Free any managed objects here.
            }
            
            disposed = true;
        }
    }
}
