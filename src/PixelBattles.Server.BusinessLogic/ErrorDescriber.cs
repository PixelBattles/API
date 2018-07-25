using PixelBattles.Server.Core;

namespace PixelBattles.Server.BusinessLogic
{
    public class ErrorDescriber
    {
        public Error DefaultError => new Error("Unknown error", "Unknown error");
    }
}
