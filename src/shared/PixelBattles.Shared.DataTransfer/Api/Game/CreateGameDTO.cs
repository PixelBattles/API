using System;

namespace PixelBattles.Shared.DataTransfer.Api.Game
{
    public class CreateGameDTO
    {
        public Guid BattleId { get; set; }

        public string Name { get; set; }

        public int Cooldown { get; set; }

        public string Type { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int ChunkSize { get; set; }

        public DateTime? StartDateUTC { get; set; }

        public DateTime? EndDateUTC { get; set; }
    }
}
