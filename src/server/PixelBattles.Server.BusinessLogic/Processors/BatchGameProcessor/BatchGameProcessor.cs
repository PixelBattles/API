using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Server.Core;
using PixelBattles.Server.DataStorage.Models;
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
        private bool disposed = false;

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

        private IUserActionCache userActionCache;

        private IServiceScopeFactory serviceScopeFactory;

        private Task updateTask;

        public BatchGameProcessor(
            Game game,
            IServiceScopeFactory serviceScopeFactory,
            IEnumerable<UserAction> pendingActions = null,
            int batchLimit = 100,
            int cacheSize = 1000)
        {
            this.batchLimit = batchLimit;
            this.userActionCache = new UserActionCache(cacheSize);

            this.game = game;
            this.state = game.State;
            this.stateChangeIndex = game.ChangeIndex ?? 0;
            this.changeIndex = game.ChangeIndex ?? 0;
            this.pixels = new Rgba32[game.Height * game.Width];
            
            this.pendingActions = new ConcurrentDictionary<int, UserAction>(
                (pendingActions ?? Enumerable.Empty<UserAction>())
                .Select(t => new KeyValuePair<int, UserAction>(t.ChangeIndex, t)));

            this.updateSemaphore = new SemaphoreSlim(1);
            this.updateCancellationTokenSource = new CancellationTokenSource();
            this.serviceScopeFactory = serviceScopeFactory;
            this.updateTask = Task.Run(UpdateStateAsync, updateCancellationTokenSource.Token);
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
                var cacheRessult = userActionCache.GetRange(fromChangeIndex, localChangeIndex);
                if (cacheRessult == null)
                {
                    return Task.FromResult(new GameDeltaResult(new Error("Cache miss", "Cache miss")));
                }
                for (int i = cacheRessult.Length - 1; i >= 0 ; i--)
                {
                    resultUserActions.Push(cacheRessult[i]);
                }
                return Task.FromResult(new GameDeltaResult(resultUserActions));
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
                ChangeIndex = actionChangeIndex,
                GameId = game.GameId,
                UserId = command.UserId
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
                    
                    await UpdateStateInternalAsync(updateCancellationTokenSource.Token);
                }
                catch (Exception)
                {
                    //log here
                }
            }
        }

        private async Task UpdateStateInternalAsync(CancellationToken cancellationToken)
        {
            int currentStateChangeIndex = stateChangeIndex;

            List<UserAction> batchPendingActions = new List<UserAction>();

            Rgba32[] newPixels = (Rgba32[])pixels.Clone();

            while (pendingActions.TryGetValue(++currentStateChangeIndex, out UserAction pendingAction))
            {
                batchPendingActions.Add(pendingAction);
            }
            
            foreach (var action in batchPendingActions)
            {
                newPixels[action.YIndex * game.Width + action.XIndex] = action.Pixel;
            }

            byte[] newState = GetBytesFromPixels(newPixels);

            var saveResult = await SaveGameAsync(game.GameId, newState, --currentStateChangeIndex, batchPendingActions, cancellationToken);
            if (!saveResult)
            {
                return;
            }

            var oldState = Interlocked.Exchange(ref state, newState);
            var oldStateChangeIndex = Interlocked.Exchange(ref stateChangeIndex, currentStateChangeIndex);

            userActionCache.Push(batchPendingActions);

            foreach (var pendingAction in batchPendingActions)
            {
                pendingActions.TryRemove(pendingAction.ChangeIndex, out UserAction ignore);
            }
        }

        private async Task<bool> SaveGameAsync(Guid gameId, byte[] state, int changeIndex, IEnumerable<UserAction> userActions, CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    using (var userActionStore = scope.ServiceProvider.GetRequiredService<IUserActionStore>())
                    using (var gameStore = scope.ServiceProvider.GetRequiredService<IGameStore>())
                    {
                        var game = await gameStore.GetGameAsync(gameId, cancellationToken);
                        game.ChangeIndex = changeIndex;
                        game.State = state;

                        var userActionEntities = userActions
                            .Select(t => new UserActionEntity
                            {
                                ChangeIndex = t.ChangeIndex,
                                GameId = t.GameId,
                                Color = t.Pixel.PackedValue,
                                UserId = t.UserId,
                                XIndex = t.XIndex,
                                YIndex = t.YIndex
                            });

                        await userActionStore.CreateBatchAsync(userActionEntities, cancellationToken);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                //log error
                return false;
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                updateCancellationTokenSource.Cancel();
                updateTask.Wait();

                
                updateCancellationTokenSource.Dispose();
                updateTask.Dispose();
                updateSemaphore.Dispose();
                userActionCache.Dispose();
            }

            disposed = true;
        }
    }
}
