using PixelBattles.Server.BusinessLogic.Models;
using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public class GameState
    {
        public byte[] State { get; set; }
        
        public IEnumerable<UserAction> PendingActions { get; set; }
    }
}
