using PixelBattles.Server.BusinessLogic.Processors.Chunk.Models;
using PixelBattles.Server.Core;
using SixLabors.ImageSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Processors.Chunk
{
    public class ChunkProcessor : IChunkProcessor
    {
        private bool disposed = false;

        private int changeIndex;

        private int batchSize;

        private byte[] state;

        private Rgba32[] pixels;

        private int stateChangeIndex;

        private int batchLimit;

        private SemaphoreSlim updateSemaphore;

        private ConcurrentDictionary<int, ChunkAction> pendingActions;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public int ChangeIndex { get; private set; }
        
        public ChunkProcessor()
        {

        }

        public Task<ProcessActionResult> ProcessActionAsync(ProcessActionCommand command)
        {
            int actionChangeIndex = Interlocked.Increment(ref changeIndex);
            ChunkAction action = new ChunkAction()
            {
                YIndex = command.YIndex,
                XIndex = command.XIndex,
                Pixel = command.Pixel,
                ChangeIndex = actionChangeIndex
            };

            if (!pendingActions.TryAdd(actionChangeIndex, action))
            {
                return Task.FromResult(new ProcessActionResult(new Error("Error while processing action", "Error while processing action")));
            }

            int currentBatchSize = Interlocked.Increment(ref batchSize);

            if (currentBatchSize > batchLimit)
            {
                updateSemaphore.Release();
            }

            var result = new ProcessActionResult(action);
            return Task.FromResult(result);
        }

        public Task<ChunkState> GetChunkStateAsync()
        {
            var chunkState = new ChunkState()
            {
                ChangeIndex = changeIndex,
                State = state,
                PendingActions = pendingActions.ToArray(),
            };
            return Task.FromResult(chunkState);
        }

        public Task<ChunkDeltaResult> GetChunkDeltaAsync(int changeIndexFrom, int changeIndexTo)
        {
            throw new NotImplementedException();
        }

        private async Task UpdateStateInternalAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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

            }

            disposed = true;
        }
    }
}
