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
        private readonly ApiClientOptions options;
        private readonly HttpClient httpClient;

        public ApiClient(ApiClientOptions options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));

            httpClient = new HttpClient
            {
                BaseAddress = new Uri(options.BaseUrl),
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
