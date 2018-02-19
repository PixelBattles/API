using Microsoft.AspNetCore.SignalR;
using PixelBattles.Server.BusinessLogic.Processors;
using System;
using System.Collections.Concurrent;

namespace PixelBattles.Server.Hub
{
    public class PixelBattleHubContext
    {
        public ConcurrentDictionary<Guid, IGameProcessor> Games { get; set; }

        public IHubContext<PixelBattleHub> HubContext { get; set; }

        public PixelBattleHubContext(
            IHubContext<PixelBattleHub> hubContext)
        {
            this.HubContext = hubContext;
            this.Games = new ConcurrentDictionary<Guid, IGameProcessor>();
        }
        

    }
}
