using PixelBattles.Shared.DataTransfer.Api.Battle;

namespace PixelBattles.Server.IntegrationTests.Clients.Api
{
    public interface IApiClient
    {
        CreateBattleResultDTO CreateBattle(CreateBattleDTO createBattleDTO);
    }
}
