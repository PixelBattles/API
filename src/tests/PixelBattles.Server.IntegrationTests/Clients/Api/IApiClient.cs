using PixelBattles.Shared.DataTransfer.Api.Battle;
using System;

namespace PixelBattles.Server.IntegrationTests.Clients.Api
{
    public interface IApiClient
    {
        CreateBattleResultDTO CreateBattle(CreateBattleDTO createBattleDTO);

        BattleDTO GetBattle(Guid battleId);
    }
}
