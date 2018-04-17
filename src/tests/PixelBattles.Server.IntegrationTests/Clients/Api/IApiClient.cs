using PixelBattles.Shared.DataTransfer.Api.Battle;
using PixelBattles.Shared.DataTransfer.Api.Game;
using System;
using System.Collections.Generic;

namespace PixelBattles.Server.IntegrationTests.Clients.Api
{
    public interface IApiClient
    {
        CreateBattleResultDTO CreateBattle(CreateBattleDTO createBattleDTO);

        CreateGameResultDTO CreateGame(CreateGameDTO createGameDTO);

        BattleDTO GetBattle(Guid battleId);

        IEnumerable<BattleDTO> GetBattles(BattleFilterDTO battleFilterDTO);

        CreateGameTokenResultDTO GetGameToken(CreateGameTokenDTO createGameTokenDTO);
    }
}
