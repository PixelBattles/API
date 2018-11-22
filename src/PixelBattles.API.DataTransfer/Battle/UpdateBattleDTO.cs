using System;

namespace PixelBattles.API.DataTransfer.Battle
{
    public class UpdateBattleDTO
    {
        public Guid BattleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
