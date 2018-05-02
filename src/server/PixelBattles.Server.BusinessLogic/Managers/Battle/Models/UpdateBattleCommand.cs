using System;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class UpdateBattleCommand
    {
        public Guid BattleId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }
    }
}
