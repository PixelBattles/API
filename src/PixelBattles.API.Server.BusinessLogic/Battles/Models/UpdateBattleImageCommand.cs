using System;

namespace PixelBattles.API.Server.BusinessLogic.Battles.Models
{
    public class UpdateBattleImageCommand
    {
        public long BattleId { get; set; }
        
        public Guid ImageId { get; set; }
    }
}