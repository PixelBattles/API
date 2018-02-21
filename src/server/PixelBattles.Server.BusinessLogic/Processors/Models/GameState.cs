using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public class GameState
    {
        public int ChangeIndex { get; set; }

        public byte[] State { get; set; }
        
        public IEnumerable<KeyValuePair<int, UserAction>> PendingActions { get; set; }
    }
}
