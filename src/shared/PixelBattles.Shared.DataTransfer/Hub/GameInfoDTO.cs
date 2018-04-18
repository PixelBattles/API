using System;

namespace PixelBattles.Shared.DataTransfer.Hub
{
    public class GameInfoDTO
    {
        public Guid GameId { get; set; }

        public int GameSizeX { get; set; }

        public int GameSizeY { get; set; }
        
        public int ChunkSizeX { get; set; }

        public int ChunkSizeY { get; set; }
    }
}
