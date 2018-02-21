using System;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class BattleFilter
    {
        public string Name { get; set; }

        public Guid? UserId { get; set; }
    }
}
