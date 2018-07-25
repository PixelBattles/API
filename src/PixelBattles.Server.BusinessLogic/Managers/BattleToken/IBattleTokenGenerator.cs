using System;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public interface IBattleTokenGenerator
    {
        string GenerateToken(Guid battleId, Guid userId);
    }
}
