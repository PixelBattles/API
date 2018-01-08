using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace PixelBattles.Server.DataStorage.Stores
{
    public class EnumLookup<TEnum> where TEnum : struct, IConvertible
    {
        private EnumLookup(TEnum @enum)
        {
            Id = (int)((object)@enum);
            Name = @enum.ToString();
        }

        protected EnumLookup() { }

        public int Id { get; set; }

        public string Name { get; set; }

        public static implicit operator EnumLookup<TEnum>(TEnum @enum)
        {
            return new EnumLookup<TEnum>(@enum);
        }

        public static implicit operator TEnum(EnumLookup<TEnum> @enumLookup)
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), @enumLookup.Id);
        }

        public static void SeedEnumValues(DbSet<EnumLookup<TEnum>> dbSet, Func<TEnum, EnumLookup<TEnum>> converter)
        {
            Enum.GetValues(typeof(TEnum))
                  .Cast<object>()
                  .Select(value => converter((TEnum)value))
                  .Where(instance => !dbSet.Any(t => t.Id == instance.Id))
                  .ToList()
                  .ForEach(instance => dbSet.Add(instance));
        }
    }
}
