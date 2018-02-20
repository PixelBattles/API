using PixelBattles.Server.DataStorage.Models;
using System;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class Game
    {
        public Guid GameId { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int? ChangeIndex { get; set; }

        public byte[] State { get; set; }
    }

    public partial class BusinessLogicMappingProfile
    {
        private void InitializeGame()
        {
            CreateMap<GameEntity, Game>();
        }
    }
}
