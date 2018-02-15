using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Server.Core;
using SixLabors.ImageSharp;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    [Register(typeof(IGameProcessor))]
    public class GameProcessor : IGameProcessor
    {
        public Guid GameId { get => Game.GameId; }

        public int Height { get => Game.Height; }

        public int Width { get => Game.Width; }

        protected Rgba32[] Pixels { get; set; }

        protected int ChangeIndex;

        protected int BaseIndex;

        protected Game Game { get; set; }

        protected ConcurrentDictionary<int, UserAction> ActionDictionary { get; set; }
        
        public GameProcessor(Game game)
        {
            Game = game ?? throw new ArgumentNullException(nameof(game));
            ActionDictionary = new ConcurrentDictionary<int, UserAction>();
        }

        public ProcessUserActionResult ProcessUserAction(ProcessUserActionCommand command)
        {
            int updatedIndex = Interlocked.Increment(ref ChangeIndex);
            UserAction action = new UserAction()
            {
                ChangeIndex = updatedIndex,
                GameId = Game.GameId,
                Height = command.Height,
                Width = command.Width,
                Pixel = command.Pixel
            };
            if (!ActionDictionary.TryAdd(updatedIndex, action))
            {
                return new ProcessUserActionResult(new Error("Error while processing action", "Error while processing action"));
            }

            var result = new ProcessUserActionResult();
            return result;
        }

        protected void UpdateState()
        {
            int currentIndex = ChangeIndex;
            Rgba32[] updatedPixels = (Rgba32[])Pixels.Clone();
            //GENERATE STATE
        }
                
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
