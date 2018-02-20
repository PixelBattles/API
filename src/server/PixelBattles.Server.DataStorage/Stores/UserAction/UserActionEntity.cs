using Microsoft.EntityFrameworkCore;
using System;

namespace PixelBattles.Server.DataStorage.Models
{
    public class UserActionEntity
    {
        public Guid GameId { get; set; }

        public Guid UserId { get; set; }

        public int ChangeIndex { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Color { get; set; }
    }

    class UserActionEntityBuilder : IBuildable
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<UserActionEntity>(action =>
            {
                action.ToTable("UserAction");
                action.HasKey(t => new { t.GameId, t.ChangeIndex });
            });
        }
    }
}