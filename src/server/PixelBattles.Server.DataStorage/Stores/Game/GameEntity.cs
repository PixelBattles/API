using Microsoft.EntityFrameworkCore;
using System;

namespace PixelBattles.Server.DataStorage.Models
{
    public class GameEntity
    {
        public Guid GameId { get; set; }

        public byte[] State { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int ChangeIndex { get; set; }
    }

    class GameEntityBuilder : IBuildable
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<GameEntity>(battle =>
            {
                battle.ToTable("Game");
                battle.HasKey(t => t.GameId);
            });
        }
    }
}