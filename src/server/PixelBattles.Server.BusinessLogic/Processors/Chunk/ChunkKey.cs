using System;

namespace PixelBattles.Server.BusinessLogic.Processors.Chunk
{
    public class ChunkKey
    {
        public Guid GameId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                return GameId.GetHashCode() ^ (X * 397) ^ (Y * 397);
            }
        }
    }
}
