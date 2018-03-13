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
            builder.Entity<HubEntity>(hub =>
            {
                hub.ToTable("Hub");
                hub.HasKey(t => t.HubId);
                hub.HasIndex(t => t.Name).IsUnique();
                hub.Property(t => t.Name).IsRequired();
            });
        }
    }
}
