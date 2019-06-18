using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PixelBattles.API.DataTransfer;
using PixelBattles.API.DataTransfer.Battles;
using PixelBattles.API.DataTransfer.Images;
using System;
using System.Net.Http;
using System.Text;
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

        public Task<CreateImageResultDTO> CreateImageAsync(CreateImageDTO createImage, byte[] data, CancellationToken cancellationToken = default)
        {
            throw new InvalidOperationException();
        }

        public async Task<BattleDTO> GetBattleAsync(long battleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var response = await _httpClient.GetAsync($"/api/battle/{battleId}"))
            {
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var battle = JsonConvert.DeserializeObject<BattleDTO>(responseContent);
                return battle;
            }
        }

        public async Task<ResultDTO> UpdateBattleImageAsync(long battleId, UpdateBattleImageDTO updateBattleImage, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(updateBattleImage), Encoding.UTF8, "application/json"))
            {
                using (var response = await _httpClient.PutAsync($"/api/battle/{battleId}/image", content))
                {
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResultDTO>(responseContent);
                    return result;
                }
            }
        }

        public Task<ResultDTO> UpdateImageAsync(Guid imageId, UpdateImageDTO updateImage, byte[] data, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}