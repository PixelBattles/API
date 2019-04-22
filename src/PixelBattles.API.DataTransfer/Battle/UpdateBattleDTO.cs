using System;

namespace PixelBattles.API.DataTransfer.Battle
{
    public class UpdateBattleDTO
    {
        public long BattleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
