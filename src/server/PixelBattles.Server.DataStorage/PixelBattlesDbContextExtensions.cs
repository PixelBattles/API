using PixelBattles.Server.DataStorage.Models;
using PixelBattles.Server.DataStorage.Stores;

namespace PixelBattles.Server.DataStorage
{
    public static class PixelBattlesDbContextExtensions
    {
        public static void EnsureSeedData(this PixelBattlesDbContext context)
        {
            SeedEnumLookups(context);
        }

        private static void SeedEnumLookups(PixelBattlesDbContext context)
        {
            EnumLookup<BattleStatusEntity>.SeedEnumValues(context.Set<EnumLookup<BattleStatusEntity>>(), @enum => @enum);
            context.SaveChanges();
        }
    }
}
