using System;

namespace PixelBattles.API.Server.BusinessLogic.Managers
{
    public interface IBattleTokenGenerator
    {
        string GenerateToken(long battleId, Guid userId);
    }
}
