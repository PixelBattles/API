using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PixelBattles.API.DataTransfer.Battle;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PixelBattles.API.Client
{
    public class ApiClient : IApiClient
    {
        private readonly ApiClientOptions _options;
        private readonly HttpClient _httpClient;

        public ApiClient(IOptions<ApiClientOptions> options)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_options.BaseUrl),
            };
        }

        public async Task<BattleDTO> GetBattleAsync(long battleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _httpClient.GetAsync("/api/battle/" + battleId);
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
