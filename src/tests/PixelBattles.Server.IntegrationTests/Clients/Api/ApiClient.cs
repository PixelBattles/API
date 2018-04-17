using Newtonsoft.Json;
using PixelBattles.Shared.DataTransfer.Api.Battle;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PixelBattles.Server.IntegrationTests.Clients.Api
{
    public class ApiClient : IApiClient
    {
        public string BackendUrl
        {
            get => httpClient.BaseAddress.AbsolutePath;
            set => httpClient.BaseAddress = new Uri(value);
        }
        private HttpClient httpClient;
        public ApiClient()
        {
            httpClient = new HttpClient();
        }

        public IEnumerable<BattleDTO> GetBattles()
        {
            return null;
        }

        public async Task<CreateBattleResultDTO> CreateBattleAsync(CreateBattleDTO createBattleDTO)
        {
            var requestContent = new StringContent(JsonConvert.SerializeObject(createBattleDTO), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/battle", requestContent);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CreateBattleResultDTO>(content);
        }

        public CreateBattleResultDTO CreateBattle(CreateBattleDTO createBattleDTO)
        {
            return CreateBattleAsync(createBattleDTO).Result;
        }

        public async Task<BattleDTO> GetBattleAsync(Guid battleId)
        {
            var response = await httpClient.GetAsync($"/api/battle/{battleId}");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BattleDTO>(content);
        }
        public BattleDTO GetBattle(Guid battleId)
        {
            return GetBattleAsync(battleId).Result;
        }
    }
}
