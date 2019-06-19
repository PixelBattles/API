using System;

namespace PixelBattles.API.Server.BusinessLogic.Battles.Models
{
    public class CreateBattleTokenCommand
    {
        public long BattleId { get; set; }

        public Guid UserId { get; set; }
    }
}
