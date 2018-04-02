using PixelBattles.Server.Core;
using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Processors.Chunk.Models
{
    public class ProcessActionResult : Result
    {
        public ChunkAction ChunkAction { get; set; }

        public ProcessActionResult(ChunkAction chunkAction) : base(succeeded: true)
        {
            this.ChunkAction = chunkAction;
        }

        public ProcessActionResult(params Error[] errors) : base(false, errors)
        {

        }

        public ProcessActionResult(IEnumerable<Error> errors) : base(false, errors)
        {

        }
    }
}
