using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.Core;

namespace PixelBattles.Server.BusinessLogic
{
    [Register(ServiceLifetime.Singleton)]
    public class ErrorDescriber
    {
        public Error DefaultError => new Error("Unknown error", "Unknown error");
    }
}
