using System;

namespace PixelBattles.API.Server.BusinessLogic.Managers
{
    public interface IBattleTokenGenerator
    {
        string GenerateToken(Guid battleId, Guid userId);
    }
}
