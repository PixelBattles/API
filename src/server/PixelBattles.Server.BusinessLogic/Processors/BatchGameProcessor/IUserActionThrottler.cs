using System;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public interface IUserActionThrottler
    {
        TimeSpan Cooldown { get; }

        bool CanUserContinue(Guid userId, DateTime actionDateTime);

        void Clear(DateTime actionDateTime);
    }
}
