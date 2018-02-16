using PixelBattles.Server.BusinessLogic.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public abstract class GameProcessor : IGameProcessor
    {
        public Guid GameId { get => Game.GameId; }

        public int Height { get => Game.Height; }

        public int Width { get => Game.Width; }

        protected Rgba32[] Pixels { get; set; }

        protected byte[] State;

        protected int ChangeIndex;

        protected int StateIndex;

        protected Game Game { get; set; }

        protected ConcurrentQueue<UserAction> ActionQueue { get; set; }
        
        public GameProcessor(Game game)
        {
            Game = game ?? throw new ArgumentNullException(nameof(game));
            ActionQueue = new ConcurrentQueue<UserAction>();
        }

        public ProcessUserActionResult ProcessUserAction(ProcessUserActionCommand command)
        {
            UserAction action = new UserAction()
            {
                GameId = Game.GameId,
                Height = command.Height,
                Width = command.Width,
                Pixel = command.Pixel
            };
            ActionQueue.Enqueue(action);

            var result = new ProcessUserActionResult();
            return result;
        }

        protected void UpdateState()
        {
            var items = ActionQueue.ToArray();

            //UPDATE INDEXES
            foreach (var item in items)
            {
                item.ChangeIndex = Interlocked.Increment(ref ChangeIndex);
                Pixels[item.Height * Width + item.Width] = item.Pixel;
            }

            byte[] newState = new byte[State.Length];
            using (MemoryStream stream = new MemoryStream())
            {
                var image = Image.LoadPixelData(this.Pixels, this.Width, this.Height);
                PngEncoder pngEncoder = new PngEncoder
                {
                    IgnoreMetadata = true,
                    PngColorType = PngColorType.Rgb
                };
                image.SaveAsPng(stream, pngEncoder);
                newState = stream.ToArray();
            }

            var oldState = Interlocked.Exchange(ref State, newState);

            foreach (var item in items)
            {
                ActionQueue.TryDequeue(out UserAction tempUserAction);
            }
        }
                
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        
        public GameState GetGameState()
        {
            var gameState = new GameState()
            {
                State = State,
                PendingActions = ActionQueue.ToArray()
            };
            return gameState;
        }
    }
}
