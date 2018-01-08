using Microsoft.EntityFrameworkCore;
using PixelBattles.Server.DataStorage.Models;
using System;
using System.Linq;
using System.Reflection;

namespace PixelBattles.Server.DataStorage
{
    public class PixelBattlesDbContext : DbContext
    {
        public PixelBattlesDbContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            BuildAllModels(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected void BuildAllModels(ModelBuilder modelBuilder)
        {
            Assembly modelAssembly = GetType().Assembly;
            modelAssembly
                .GetTypes()
                .Where(type => type.GetInterfaces().Contains(typeof(IBuildable)))
                .ToList()
                .ForEach(type =>
                {
                    IBuildable builder = Activator.CreateInstance(type) as IBuildable;
                    builder.Build(modelBuilder);
                });
        }
    }
}
