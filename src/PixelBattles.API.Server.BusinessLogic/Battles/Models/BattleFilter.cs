using System;

namespace PixelBattles.API.Server.BusinessLogic.Battles.Models
{
    public class BattleFilter
    {
        public string Name { get; set; }

        public Guid? UserId { get; set; }

        public int From { get; set; }
    }
}
