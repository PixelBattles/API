using Newtonsoft.Json;
using PixelBattles.Shared.DataTransfer.Api.Battle;
using PixelBattles.Shared.DataTransfer.Api.Game;
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

        public async Task<CreateGameResultDTO> CreateGameAsync(CreateGameDTO createGameDTO)
        {
            var requestContent = new StringContent(JsonConvert.SerializeObject(createGameDTO), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/game", requestContent);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CreateGameResultDTO>(content);
        }

        public CreateGameResultDTO CreateGame(CreateGameDTO createGameDTO)
        {
            return CreateGameAsync(createGameDTO).Result;
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

        public async Task<IEnumerable<BattleDTO>> GetBattlesAsync(BattleFilterDTO battleFilterDTO)
        {
            var response = await httpClient.GetAsync($"/api/battle");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<BattleDTO>>(content);
        }

        public IEnumerable<BattleDTO> GetBattles(BattleFilterDTO battleFilterDTO)
        {
            return GetBattlesAsync(battleFilterDTO).Result;
        }

        public async Task<CreateGameTokenResultDTO> GetGameTokenAsync(CreateGameTokenDTO createGameTokenDTO)
        {
            var requestContent = new StringContent(JsonConvert.SerializeObject(createGameTokenDTO), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/game/token", requestContent);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CreateGameTokenResultDTO>(content);
        }

        public CreateGameTokenResultDTO GetGameToken(CreateGameTokenDTO createGameTokenDTO)
        {
            return GetGameTokenAsync(createGameTokenDTO).Result;
        }
    }
}
