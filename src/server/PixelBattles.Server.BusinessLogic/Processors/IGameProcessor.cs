using System;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public interface IGameProcessor : IDisposable
    {
        ProcessUserActionResult ProcessUserAction(ProcessUserActionCommand command);

        GameState GetGameState();
    }
}
