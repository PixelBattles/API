using System;

namespace PixelBattles.API.DataTransfer.Battles
{
    public class BattleDTO
    {
        public long BattleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ImageId { get; set; }
        public BattleSettingsDTO Settings { get; set; }
        public DateTime StartDateUTC { get; set; }
        public DateTime EndDateUTC { get; set; }
    }
}