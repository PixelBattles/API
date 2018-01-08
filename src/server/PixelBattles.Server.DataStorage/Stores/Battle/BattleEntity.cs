using Microsoft.EntityFrameworkCore;
using System;

namespace PixelBattles.Server.DataStorage.Models
{
    public class BattleEntity
    {
        public Guid BattleId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
                
        public BattleStatusEntity Status { get; set; }
    }

    class BattleEntityBuilder : IBuildable
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<BattleEntity>(battle =>
            {
                battle.ToTable("Battle");
                battle.HasKey(t => t.BattleId);
            });
        }
    }
}