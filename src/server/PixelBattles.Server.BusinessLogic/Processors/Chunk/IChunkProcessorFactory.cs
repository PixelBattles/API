using System.Threading.Tasks;

namespace PixelBattles.Server.BusinessLogic.Processors.Chunk
{
    public interface IChunkProcessorFactory
    {
        Task<IChunkProcessor> CreateAsync(ChunkKey chunkKey);
    }
}
