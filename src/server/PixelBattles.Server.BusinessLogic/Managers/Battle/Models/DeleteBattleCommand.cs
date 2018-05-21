using System;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class DeleteBattleCommand
    {
        public Guid BattleId { get; set; }

        public Guid UserId { get; set; }
    }
}
