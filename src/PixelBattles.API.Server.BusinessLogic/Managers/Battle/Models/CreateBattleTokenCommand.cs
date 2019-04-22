using System;

namespace PixelBattles.API.Server.BusinessLogic.Models
{
    public class CreateBattleTokenCommand
    {
        public long BattleId { get; set; }

        public Guid UserId { get; set; }
    }
}
