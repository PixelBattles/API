using System;

namespace PixelBattles.Server.DataStorage.Models
{
    public class BattleEntityFilter
    {
        public string Name { get; set; }

        public Guid? UserId { get; set; }
    }
}
