using Microsoft.AspNetCore.SignalR;
using PixelBattles.Server.BusinessLogic.Processors;
using System;
using System.Collections.Concurrent;

namespace PixelBattles.Server.Hub
{
    public class PixelBattleHubContext
    {
        protected ConcurrentDictionary<Guid, IGameProcessor> Games { get; set; }
        
        protected IHubContext<PixelBattleHub> HubContext { get; set; }

        public PixelBattleHubContext(
            IHubContext<PixelBattleHub> hubContext)
        {
            this.HubContext = hubContext;
            this.Games = new ConcurrentDictionary<Guid, IGameProcessor>();
        }

        public IGameProcessor GetGame(Guid gameId)
        {
            if (Games.TryGetValue(gameId, out IGameProcessor game))
            {
                return game;
            }
            return null;
        }

        public bool ContainsGame(Guid gameId)
        {
            return Games.ContainsKey(gameId);
        }
    }
}
