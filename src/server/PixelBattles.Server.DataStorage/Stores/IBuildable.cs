using Microsoft.EntityFrameworkCore;

namespace PixelBattles.Server.DataStorage.Models
{
    public interface IBuildable
    {
        void Build(ModelBuilder builder);
    }
}
