using PixelBattles.Server.BusinessLogic.Processors.Chunk.Models;
using System;
using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Processors.Chunk
{
    public interface IChunkProcessor : IDisposable
    {
        int Width { get; }

        int Height { get; }

        int ChangeIndex { get; }
        
        Task<ProcessActionResult> ProcessActionAsync(ProcessActionCommand command);

        Task<ChunkState> GetChunkStateAsync();

        Task<ChunkDeltaResult> GetChunkDeltaAsync(int changeIndexFrom, int changeIndexTo);
    }
}
