using PixelBattles.Server.BusinessLogic.Processors.Chunk;
using PixelBattles.Server.BusinessLogic.Processors.Chunk.Models;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public class ChunkContainer
    {
        protected ConcurrentDictionary<ChunkKey, IChunkProcessor> chunks;

        protected ConcurrentDictionary<ChunkKey, IChunkProcessor> inactiveChunks;

        protected IChunkProcessorFactory chunkProcessorFactory;

        public ChunkContainer(IChunkProcessorFactory chunkProcessorFactory)
        {
            this.chunkProcessorFactory = chunkProcessorFactory;

            chunks = new ConcurrentDictionary<ChunkKey, IChunkProcessor>();
            inactiveChunks = new ConcurrentDictionary<ChunkKey, IChunkProcessor>();
        }

        public async Task<ProcessActionResult> ProcessActionAsync(ChunkKey chunkKey, ProcessActionCommand command)
        {
            var chunkProcessor = await GetChunkProcessorAsync(chunkKey);
            return await chunkProcessor.ProcessActionAsync(command);
        }

        protected async Task<IChunkProcessor> GetChunkProcessorAsync(ChunkKey chunkKey)
        {
            if (chunks.TryGetValue(chunkKey, out IChunkProcessor chunkProcessor))
            {
                if (inactiveChunks.TryRemove(chunkKey, out chunkProcessor))
                {
                    chunkProcessor = chunks.GetOrAdd(chunkKey, chunkProcessor);
                }
                else
                {
                    var chunk = await chunkProcessorFactory.CreateAsync(chunkKey);
                    chunkProcessor = chunks.GetOrAdd(chunkKey, chunk);
                }
            }
            return chunkProcessor;
        }

        protected async Task UnloadInactiveChunksAsync()
        {
            DateTime dateTimeToCompare = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(1));
            foreach (var chunk in chunks)
            {
                if (chunk.Value.LastUpdateTime < dateTimeToCompare)
                {
                    inactiveChunks.TryAdd(chunk.Key, chunk.Value);
                }
            }

            foreach (var chunk in inactiveChunks)
            {
                chunks.TryRemove(chunk.Key, out var ignored);
            }
        }
    }
}
