using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Processors.Chunk.Models
{
    public class ChunkState
    {
        public int ChangeIndex { get; set; }

        public byte[] State { get; set; }

        public IEnumerable<KeyValuePair<int, ChunkAction>> PendingActions { get; set; }
    }
}
