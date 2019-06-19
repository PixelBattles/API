using System;

namespace PixelBattles.API.Server.BusinessLogic.Battles.Models
{
    public class DeleteBattleCommand
    {
        public long BattleId { get; set; }

        public Guid UserId { get; set; }
    }
}
