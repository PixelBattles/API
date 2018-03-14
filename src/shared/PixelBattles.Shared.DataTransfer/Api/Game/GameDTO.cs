﻿using System;

namespace PixelBattles.Shared.DataTransfer.Api.Game
{
    public class GameDTO
    {
        public Guid GameId { get; set; }

        public Guid BattleId { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Cooldown { get; set; }

        public int ChangeIndex { get; set; }
                
        public DateTime? StartDateUTC { get; set; }

        public DateTime? EndDateUTC { get; set; }
    }
}
