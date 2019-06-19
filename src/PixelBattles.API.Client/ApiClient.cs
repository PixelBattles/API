using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PixelBattles.API.DataTransfer;
using PixelBattles.API.DataTransfer.Battles;
using PixelBattles.API.DataTransfer.Images;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<CreateImageResultDTO> CreateImageAsync(CreateImageDTO createImage, byte[] data, string fileName, string contentType, CancellationToken cancellationToken = default)
        {
            using (var content = new MultipartFormDataContent())
            using (var imageContent = new ByteArrayContent(data))
            using (var nameContent = new StringContent(createImage.Name, Encoding.UTF8))
            using (var descriptionContent = new StringContent(createImage.Description, Encoding.UTF8))
            {
                imageContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                content.Add(imageContent, "file", fileName);
                content.Add(nameContent, "name");
                content.Add(descriptionContent, "description");
                using (var response = await _httpClient.PostAsync("/api/image", content, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CreateImageResultDTO>(responseContent);
                    return result;
                }
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
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
        
        public async Task<ResultDTO> UpdateImageAsync(Guid imageId, UpdateImageDTO updateImage, byte[] data, string fileName, string contentType, CancellationToken cancellationToken = default)
        {
            using (var content = new MultipartFormDataContent())
            using (var imageContent = new ByteArrayContent(data))
            using (var nameContent = new StringContent(updateImage.Name, Encoding.UTF8))
            using (var descriptionContent = new StringContent(updateImage.Description, Encoding.UTF8))
            {
                imageContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                content.Add(imageContent, "file", fileName);
                content.Add(nameContent, "name");
                content.Add(descriptionContent, "description");
                using (var response = await _httpClient.PutAsync($"/api/image/{imageId}", content, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResultDTO>(responseContent);
                    return result;
                }
            }
        }
    }
}