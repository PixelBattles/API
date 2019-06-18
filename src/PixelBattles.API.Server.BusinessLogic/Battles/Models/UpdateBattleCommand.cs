using System;

namespace PixelBattles.API.Server.BusinessLogic.Battles.Models
{
    public class UpdateBattleCommand
    {
        public long BattleId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public Guid UserId { get; set; }
    }
}