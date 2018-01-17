using Microsoft.EntityFrameworkCore;
using System;

namespace PixelBattles.Server.DataStorage.Models
{
    public class ActionEntity
    {
        public Guid BattleId { get; set; }

        public Guid UserId { get; set; }

        public int ChangeIndex { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Color { get; set; }
    }

    class ActionEntityBuilder : IBuildable
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<ActionEntity>(action =>
            {
                action.ToTable("Action");
                action.HasKey(t => new { t.BattleId, t.ChangeIndex });
            });
        }
    }
}