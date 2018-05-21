using PixelBattles.Shared.DataTransfer.Api.Game;
using System;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
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

        public async Task<GameDTO> GetGameAsync(Guid gameId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await httpClient.GetAsync("/api/game/" + gameId);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var game = JsonConvert.DeserializeObject<GameDTO>(responseContent);
                return game;
            }
            throw new InvalidOperationException();
        }
    }
}
