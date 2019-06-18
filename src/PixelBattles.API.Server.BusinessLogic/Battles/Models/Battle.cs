using System;

namespace PixelBattles.API.Server.BusinessLogic.Battles.Models
{
    public class Battle
    {
        public long BattleId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? ImageId { get; set; }

        public BattleSettings Settings { get; set; }

        public DateTime StartDateUTC { get; set; }

        public DateTime EndDateUTC { get; set; }
    }
}