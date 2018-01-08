using Microsoft.EntityFrameworkCore;
using PixelBattles.Server.DataStorage.Stores;

namespace PixelBattles.Server.DataStorage.Models
{
    public enum BattleStatusEntity
    {
        Waiting = 0,
        Running = 1,
        Finished = 2
    }

    class BattleStatusEntityBuilder : IBuildable
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<EnumLookup<BattleStatusEntity>>(battleStatus =>
            {
                battleStatus.ToTable("BattleStatus");
                battleStatus.HasKey(t => t.Id);
                battleStatus.Property(t => t.Id).ValueGeneratedNever();
            });
        }
    }
}
