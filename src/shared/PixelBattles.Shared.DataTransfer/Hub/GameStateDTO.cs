using System.Collections.Generic;

namespace PixelBattles.Shared.DataTransfer.Hub
{
    public class GameStateDTO
    {
        public byte[] State { get; set; }

        public IEnumerable<UserActionDTO> PendingActions { get; set; }
    }
}
