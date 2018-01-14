using Microsoft.AspNetCore.SignalR;
using PixelBattles.Server.Hub.Models;
using System;
using System.Collections.Concurrent;

namespace PixelBattles.Server.Hub
{
    public class PixelBattleHubContext
    {
        protected ConcurrentDictionary<Guid,Battle> Battles { get; set; }

        protected IHubContext<PixelBattleHub> HubContext { get; set; }

        public PixelBattleHubContext(
            IHubContext<PixelBattleHub> hubContext)
        {
            this.HubContext = hubContext;
            this.Battles = new ConcurrentDictionary<Guid, Battle>();
        }
    }
}
