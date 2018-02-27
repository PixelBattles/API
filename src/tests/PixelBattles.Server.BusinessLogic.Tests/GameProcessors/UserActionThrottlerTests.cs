using PixelBattles.Server.BusinessLogic.Processors;
using System;
using System.Globalization;
using Xunit;

namespace PixelBattles.Server.BusinessLogic.Tests.GameProcessors
{
    public class UserActionThrottlerTests
    {
        private DateTime defaultDateTime;

        private TimeSpan defaultTimeSpan;

        private Guid defaultUserId;

        public UserActionThrottlerTests()
        {
            defaultDateTime = DateTime.ParseExact("2011-03-21 13:26", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            defaultTimeSpan = TimeSpan.FromMinutes(1);
            defaultUserId = Guid.NewGuid();
        }

        [Fact]
        public void UserActionThrottler_AllowToContinue_ForUserFirstAction()
        {
            IUserActionThrottler userActionThrottler = new UserActionThrottler(defaultTimeSpan);
            Assert.True(userActionThrottler.CanUserContinue(defaultUserId, defaultDateTime));
        }

        [Fact]
        public void UserActionThrottler_NotAllowToContinue_ForUserActionWithinCooldown()
        {
            IUserActionThrottler userActionThrottler = new UserActionThrottler(defaultTimeSpan);
            userActionThrottler.CanUserContinue(defaultUserId, defaultDateTime);
            Assert.False(userActionThrottler.CanUserContinue(defaultUserId, defaultDateTime.Add(defaultTimeSpan.Divide(2))));
        }

        [Fact]
        public void UserActionThrottler_AllowToContinue_ForUserActionAfterCooldown()
        {
            IUserActionThrottler userActionThrottler = new UserActionThrottler(defaultTimeSpan);
            userActionThrottler.CanUserContinue(defaultUserId, defaultDateTime);
            Assert.True(userActionThrottler.CanUserContinue(defaultUserId, defaultDateTime.Add(defaultTimeSpan).Add(defaultTimeSpan.Divide(2))));
        }

        [Fact]
        public void UserActionThrottler_AllowToContinue_ForAnotherUserWithinCooldown()
        {
            Guid anotherUserId = Guid.NewGuid();
            IUserActionThrottler userActionThrottler = new UserActionThrottler(defaultTimeSpan);
            userActionThrottler.CanUserContinue(defaultUserId, defaultDateTime);
            Assert.True(userActionThrottler.CanUserContinue(anotherUserId, defaultDateTime));
        }

        [Fact]
        public void UserActionThrottler_ClearDoNothing_WhenNoUserAction()
        {
            IUserActionThrottler userActionThrottler = new UserActionThrottler(defaultTimeSpan);
            userActionThrottler.Clear(defaultDateTime);
        }

        [Fact]
        public void UserActionThrottler_ClearUserAction_ForUserActionAfterCooldown()
        {
            IUserActionThrottler userActionThrottler = new UserActionThrottler(defaultTimeSpan);
            userActionThrottler.CanUserContinue(defaultUserId, defaultDateTime);
            userActionThrottler.Clear(defaultDateTime.Add(defaultTimeSpan).Add(defaultTimeSpan.Divide(2)));
            Assert.True(userActionThrottler.CanUserContinue(defaultUserId, defaultDateTime.Add(defaultTimeSpan).Add(defaultTimeSpan)));
        }
    }
}
