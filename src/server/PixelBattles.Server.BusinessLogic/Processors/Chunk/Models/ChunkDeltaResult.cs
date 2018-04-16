using PixelBattles.Server.Core;
using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Processors.Chunk.Models
{
    public class ChunkDeltaResult : Result
    {
        public IEnumerable<ChunkAction> ChunkActions { get; set; }

        public ChunkDeltaResult(IEnumerable<ChunkAction> chunkActions) : base(succeeded: true)
        {
            this.ChunkActions = chunkActions;
        }

        public ChunkDeltaResult(params Error[] errors) : base(false, errors)
        {

        }

        public ChunkDeltaResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}
