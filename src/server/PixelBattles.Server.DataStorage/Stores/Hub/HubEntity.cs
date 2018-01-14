using Microsoft.EntityFrameworkCore;
using System;

namespace PixelBattles.Server.DataStorage.Models
{
    public class HubEntity
    {
        public Guid HubId { get; set; }

        public string Name { get; set; }
    }

    class HubEntityBuilder : IBuildable
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<HubEntity>(battle =>
            {
                battle.ToTable("Hub");
                battle.HasKey(t => t.HubId);
            });
        }
    }
}
