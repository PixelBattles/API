using PixelBattles.Server.BusinessLogic.Processors;
using PixelBattles.Server.BusinessLogic.Tests.GameProcessors.Helpers;
using SixLabors.ImageSharp;
using System;
using Xunit;

namespace PixelBattles.Server.BusinessLogic.Tests.GameProcessors.Tests
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
            int changeIndex = 1;
            int sizeLimit = 10;
            IUserActionCache userActionCache = new UserActionCache(sizeLimit);
            var userActionGenerator = new UserActionGenerator(Guid.NewGuid(), changeIndex);
            userActionCache.Push(userActionGenerator.GetUserAction(Guid.NewGuid(),Rgba32.White));
        }

        [Fact]
        public void UserActionCache_ReturnResult_WithSameFromAndToBorders()
        {
            int changeIndex = 1;
            int sizeLimit = 10;
            IUserActionCache userActionCache = new UserActionCache(sizeLimit);
            var userActionGenerator = new UserActionGenerator(Guid.NewGuid(), changeIndex);
            userActionCache.Push(userActionGenerator.GetUserAction(Guid.NewGuid(), Rgba32.White));
            var result = userActionCache.GetRange(changeIndex, changeIndex);
            Assert.NotNull(result);
            Assert.Equal(result.Length, 1);
            Assert.Equal(result[0].ChangeIndex, changeIndex);
        }

        [Fact]
        public void UserActionCache_ReturnNull_WhenCacheShifted()
        {
            int changeIndex = 1;
            int sizeLimit = 1;
            IUserActionCache userActionCache = new UserActionCache(sizeLimit);
            var userActionGenerator = new UserActionGenerator(Guid.NewGuid(), changeIndex);
            userActionCache.Push(userActionGenerator.GetUserAction(Guid.NewGuid(), Rgba32.White));
            userActionCache.Push(userActionGenerator.GetUserAction(Guid.NewGuid(), Rgba32.White));
            var notFoundResult = userActionCache.GetRange(changeIndex, changeIndex);
            Assert.Null(notFoundResult);
        }

        [Fact]
        public void UserActionCache_ReturnResult_WithNewShiftedResults()
        {
            int changeIndex = 1;
            int sizeLimit = 1;
            IUserActionCache userActionCache = new UserActionCache(sizeLimit);
            var userActionGenerator = new UserActionGenerator(Guid.NewGuid(), changeIndex);
            userActionCache.Push(userActionGenerator.GetUserAction(Guid.NewGuid(), Rgba32.White));
            userActionCache.Push(userActionGenerator.GetUserAction(Guid.NewGuid(), Rgba32.White));
            var result = userActionCache.GetRange(++changeIndex, changeIndex);
            Assert.NotNull(result);
            Assert.Equal(result.Length, 1);
            Assert.Equal(result[0].ChangeIndex, changeIndex);
        }
    }
}
