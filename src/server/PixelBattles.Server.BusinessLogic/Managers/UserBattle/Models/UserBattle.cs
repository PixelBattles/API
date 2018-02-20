using System;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class UserBattle
    {
        public Guid BattleId { get; set; }

        public Guid UserId { get; set; }
    }

    public partial class BusinessLogicMappingProfile
    {
        private void InitializeUserBattle()
        {
        }
    }
}
