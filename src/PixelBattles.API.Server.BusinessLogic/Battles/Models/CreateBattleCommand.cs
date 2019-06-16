using System;

namespace PixelBattles.API.Server.BusinessLogic.Battles.Models
{
    public class CreateBattleCommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }
    }
}
