using System;

namespace PixelBattles.API.DataTransfer.Battle
{
    public class CreateBattleResultDTO : ResultDTO
    {
        public Guid? BattleId { get; set; }
    }
}
