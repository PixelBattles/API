using System;
using System.Collections.Generic;
using System.Text;

namespace PixelBattles.Server.IntegrationTests.Clients.Hub
{
    public class HubClient : IHubClient
    {
        protected string backendUrl;
        public HubClient(string backendUrl)
        {
            this.backendUrl = backendUrl;
        }
    }
}
