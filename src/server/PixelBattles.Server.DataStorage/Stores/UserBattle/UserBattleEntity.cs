using Microsoft.EntityFrameworkCore;
using System;

namespace PixelBattles.Server.DataStorage.Models
{
    public class UserBattleEntity
    {
        public Guid BattleId { get; set; }

        public Guid UserId { get; set; }
    }

    class UserBattleEntityBuilder : IBuildable
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<UserBattleEntity>(userBattle =>
            {
                userBattle.ToTable("UserBattle");
                userBattle.HasKey(t => new { t.BattleId, t.UserId });
            });
        }
    }
}