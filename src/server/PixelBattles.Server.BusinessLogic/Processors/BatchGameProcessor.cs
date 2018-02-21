using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Server.Core;
using PixelBattles.Server.DataStorage.Stores;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public sealed class BatchGameProcessor : IGameProcessor
    {
        private Game game;

        private int changeIndex;

        private int batchSize;

        private int batchLimit;

        private byte[] state;

        private int stateChangeIndex;

        private Rgba32[] pixels;

        private ConcurrentDictionary<int, UserAction> pendingActions;

        private SemaphoreSlim updateSemaphore;

        private CancellationTokenSource updateCancellationTokenSource;

        private IUserActionStore userActionStore;

        public BatchGameProcessor(
            Game game,
            IUserActionStore userActionStore)
        {
            this.game = game;
            this.userActionStore = userActionStore;
        }
        
        public void Dispose()
        {
        }
        
        public Task<GameDeltaResult> GetGameDeltaAsync(int fromChangeIndex, int toChangeIndex)
        {
            if (toChangeIndex > changeIndex)
            {
                return Task.FromResult(new GameDeltaResult(new Error("Cache miss", "Cache miss")));
            }

            Stack<UserAction> resultUserActions = new Stack<UserAction>(toChangeIndex - fromChangeIndex + 1);

            int localChangeIndex = toChangeIndex;

            while (localChangeIndex >= fromChangeIndex)
            {
                if (pendingActions.TryGetValue(localChangeIndex, out UserAction userAction))
                {
                    resultUserActions.Push(userAction);
                }
                else
                {
                    break;
                }
                localChangeIndex++;
            }

            if (localChangeIndex >= fromChangeIndex)
            {
                //further cache search
                //not resolved for now
                return Task.FromResult(new GameDeltaResult(new Error("Cache miss", "Cache miss")));
            }

            return Task.FromResult(new GameDeltaResult(resultUserActions));
        }
        
        public Task<ProcessUserActionResult> ProcessUserActionAsync(ProcessUserActionCommand command)
        {
            int actionChangeIndex = Interlocked.Increment(ref changeIndex);
            UserAction action = new UserAction()
            {
                YIndex = command.YIndex,
                XIndex = command.XIndex,
                Pixel = command.Pixel,
                ChangeIndex = actionChangeIndex
            };

            if (!pendingActions.TryAdd(actionChangeIndex, action))
            {
                return Task.FromResult(new ProcessUserActionResult(new Error("Error while processing action", "Error while processing action")));
            }

            int currentBatchSize = Interlocked.Increment(ref batchSize);

            if (currentBatchSize > batchLimit)
            {
                updateSemaphore.Release();
            }

            var result = new ProcessUserActionResult(action);
            return Task.FromResult(result);
        }

        public Task<GameState> GetGameStateAsync()
        {
            var gameState = new GameState()
            {
                ChangeIndex = changeIndex,
                State = state,
                PendingActions = pendingActions.ToList(),
            };
            return Task.FromResult(gameState);
        }

        private async Task UpdateStateAsync()
        {
            while (true)
            {
                await updateSemaphore.WaitAsync(updateCancellationTokenSource.Token);
                try
                {
                    if (!(batchSize > batchLimit))
                    {
                        continue;
                    }
                    
                    UpdateState();
                }
                catch (Exception)
                {
                    //log here
                }
            }
        }

        private void UpdateState()
        {
            int currentStateChangeIndex = stateChangeIndex;
            List<UserAction> batchPendingActions = new List<UserAction>();

            while (pendingActions.TryGetValue(++currentStateChangeIndex, out UserAction pendingAction))
            {
                batchPendingActions.Add(pendingAction);
            }

            //Save not implemented
            
            foreach (var action in batchPendingActions)
            {
                pixels[action.YIndex * game.Width + action.XIndex] = action.Pixel;
            }

            byte[] newState = GetBytesFromPixels(pixels);

            var oldState = Interlocked.Exchange(ref state, newState);
            var oldStateChangeIndex = Interlocked.Exchange(ref stateChangeIndex, --currentStateChangeIndex);

            foreach (var pendingAction in batchPendingActions)
            {
                pendingActions.TryRemove(pendingAction.ChangeIndex, out UserAction ignore);
            }
        }

        private Rgba32[] GetPixelsFromBytes(byte[] imageArray)
        {
            Rgba32[] tempPixels = new Rgba32[game.Height * game.Width];
            IImageDecoder imageDecoder = new PngDecoder()
            {
                IgnoreMetadata = true
            };

            var image = Image.Load(imageArray, imageDecoder);
            for (int y = 0; y < game.Height; y++)
            {
                for (int x = 0; x < game.Width; x++)
                {
                    tempPixels[y * game.Width + x] = image[x, y];
                }
            }
            return tempPixels;
        }

        private byte[] GetBytesFromPixels(Rgba32[] pixelArray)
        {
            byte[] byteArray;
            using (MemoryStream stream = new MemoryStream())
            {
                var image = Image.LoadPixelData(pixelArray, game.Width, game.Height);
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
    }
}
