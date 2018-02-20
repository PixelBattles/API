using System;

namespace PixelBattles.Shared.DataTransfer.Api.Game
{
    public class GameDTO
    {
        public Guid GameId { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int? ChangeIndex { get; set; }

        public string ImageURL { get; set; }
    }
}
