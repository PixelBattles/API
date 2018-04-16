using System;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class CreateGameTokenCommand
    {
        public Guid GameId { get; set; }

        public Guid UserId { get; set; }
    }
}
