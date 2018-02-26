using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using PixelBattles.Server.BusinessLogic.Models;

namespace PixelBattles.Server.BusinessLogic.Tests.GameProcessors.Helpers
{
    public class UserActionGenerator
    {
        public int StartChangeIndex { get; set; }

        public Guid GameId { get; set; }

        public UserActionGenerator(Guid gameId, int startChangeIndex = 1)
        {
            this.StartChangeIndex = startChangeIndex;
            this.GameId = gameId;
        }

        public UserAction GetUserAction(Guid userId, Rgba32 rgba32, int xIndex = 0, int yIndex = 0)
        {
            return new UserAction
            {
                ChangeIndex = StartChangeIndex++,
                GameId = GameId,
                XIndex = xIndex,
                YIndex = yIndex,
                UserId = userId,
                Pixel = rgba32
            };
        }

        public IEnumerable<UserAction> GetUserActions(int count, Guid userId, Rgba32 rgba32, int xIndex = 0, int yIndex = 0)
        {
            return Enumerable
                .Range(0, count)
                .Select(t => new UserAction
                {
                    ChangeIndex = StartChangeIndex++,
                    GameId = GameId,
                    XIndex = xIndex,
                    YIndex = yIndex,
                    UserId = userId,
                    Pixel = rgba32
                });
        }
    }
}
