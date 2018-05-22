using Newtonsoft.Json;
using PixelBattles.Shared.DataTransfer.Api.Battle;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.Server.Client
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;

        public ApiClient(string baseUrl)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
            };
        }

        public async Task<BattleDTO> GetBattleAsync(Guid battleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await httpClient.GetAsync("/api/battle/" + battleId);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var game = JsonConvert.DeserializeObject<BattleDTO>(responseContent);
                return game;
            }
            throw new InvalidOperationException();
        }
    }
}
