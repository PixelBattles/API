using System;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public interface IGameTokenGenerator
    {
        string GenerateToken(Guid gameId, Guid userId);
    }
}
