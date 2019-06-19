using System;

namespace PixelBattles.API.Server.BusinessLogic.BattleToken
{
    public interface IBattleTokenGenerator
    {
        string GenerateToken(long battleId, Guid userId);
    }
}
