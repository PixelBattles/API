using System.Collections.Generic;

namespace PixelBattles.Shared.DataTransfer.Hub
{
    public class GameDeltaResultDTO : ResultDTO
    {
        public IEnumerable<UserActionDTO> UserActions { get; set; }
    }
}
