using System;

namespace PixelBattles.Shared.DataTransfer.Api.Game
{
    public class CreateGameResultDTO : ResultDTO
    {
        public Guid? GameId { get; set; }
    }
}
