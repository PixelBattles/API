using System;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class CreateBattleCommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }
    }
}
