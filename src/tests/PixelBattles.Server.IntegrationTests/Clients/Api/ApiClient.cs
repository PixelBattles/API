using PixelBattles.Shared.DataTransfer.Api.Battle;
using System.Collections.Generic;

namespace PixelBattles.Server.IntegrationTests.Clients.Api
{
    public class ApiClient : IApiClient
    {
        public string BackendUrl { get; set; }
        public ApiClient(string backendUrl = null)
        {
            this.BackendUrl = backendUrl;
        }

        public IEnumerable<BattleDTO> GetBattles()
        {
            return null;
        }
    }
}
