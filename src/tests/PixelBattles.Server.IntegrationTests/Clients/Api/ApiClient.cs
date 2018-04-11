using System;
using System.Collections.Generic;
using System.Text;

namespace PixelBattles.Server.IntegrationTests.Clients.Api
{
    public class ApiClient : IApiClient
    {
        protected string backendUrl;
        public ApiClient(string backendUrl)
        {
            this.backendUrl = backendUrl;
        }
    }
}
