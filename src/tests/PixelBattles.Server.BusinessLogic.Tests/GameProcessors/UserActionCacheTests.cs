using PixelBattles.Server.BusinessLogic.Processors;
using SixLabors.ImageSharp;
using System;
using Xunit;

namespace PixelBattles.Server.BusinessLogic.Tests
{
    public class UserActionCacheTests
    {
        [Fact]
        public void UserActionCache_CanBeCreated_WithValidSize()
        {
            int sizeLimit = 10;
            UserActionCache userActionCache = new UserActionCache(sizeLimit);
            Assert.Equal(userActionCache.SizeLimit, sizeLimit);
        }

        [Fact]
        public void UserActionCache_CanBeCleared_WhileEmpty()
        {
            int sizeLimit = 10;
            IUserActionCache userActionCache = new UserActionCache(sizeLimit);
            userActionCache.Clear();
            Assert.Equal(userActionCache.SizeLimit, sizeLimit);
        }

        [Fact]
        public void UserActionCache_ReturnNull_WhenCannotBeResolved()
        {
            int sizeLimit = 10;
            IUserActionCache userActionCache = new UserActionCache(sizeLimit);
            var result = userActionCache.GetRange(1,2);
            Assert.Null(result);
        }

        [Fact]
        public void UserActionCache_CanPush_WhileEmpty()
        {
            int changeIndex = 11;
            int sizeLimit = 10;
            IUserActionCache userActionCache = new UserActionCache(sizeLimit);
            userActionCache.Push(new UserAction
            {
                ChangeIndex = ++changeIndex,
                Pixel = new Rgba32(0),
                XIndex = 0,
                YIndex = 0
            });
        }

        [Fact]
        public void UserActionCache_ReturnResult_WithSameFromAndToBorders()
        {
            int changeIndex = 11;
            int sizeLimit = 10;
            IUserActionCache userActionCache = new UserActionCache(sizeLimit);
            userActionCache.Push(new UserAction
            {
                ChangeIndex = ++changeIndex,
                Pixel = new Rgba32(0),
                XIndex = 0,
                YIndex = 0
            });
            var result = userActionCache.GetRange(12, 12);
            Assert.NotNull(result);
            Assert.Equal(result.Length, 1);
            Assert.Equal(result[0].ChangeIndex, 12);
        }
    }
}
