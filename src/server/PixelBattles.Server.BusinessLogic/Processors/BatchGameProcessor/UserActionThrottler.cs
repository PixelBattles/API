using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public sealed class UserActionThrottler : IUserActionThrottler
    {
        private TimeSpan cooldown;

        public TimeSpan Cooldown
        {
            get { return cooldown; }
        }
        
        private ConcurrentDictionary<Guid, UserLastAction> userActions;

        public UserActionThrottler(TimeSpan cooldown)
        {
            this.cooldown = cooldown;
            this.userActions = new ConcurrentDictionary<Guid, UserLastAction>();
        }
        
        public bool CanUserContinue(Guid userId, DateTime actionDateTime)
        {
            var newUserAction = new UserLastAction(userId, actionDateTime);
            
            var resultUserAction = userActions.AddOrUpdate(
                key: userId,
                addValue: newUserAction,
                updateValueFactory: (key, userLastAction) => 
                    userLastAction.ActionDateTime.Add(cooldown) > actionDateTime ? userLastAction : newUserAction);

            return ReferenceEquals(newUserAction, resultUserAction);
        }

        public void Clear(DateTime actionDateTime)
        {
            foreach (var userAction in userActions)
            {
                if (userAction.Value.ActionDateTime.Add(cooldown) < actionDateTime)
                {
                    ((ICollection<KeyValuePair<Guid, UserLastAction>>)userActions).Remove(userAction);
                }
            }
        }

        internal class UserLastAction : IEquatable<UserLastAction>
        {
            public Guid UserId { get; private set; }

            public DateTime ActionDateTime { get; private set; }

            public UserLastAction(Guid userId, DateTime actionDateTime)
            {
                UserId = userId;
                ActionDateTime = actionDateTime;
            }

            public bool Equals(UserLastAction userLastAction)
            {
                return UserId == userLastAction.UserId 
                    && ActionDateTime == userLastAction.ActionDateTime;
            }

            public override bool Equals(object obj)
            {
                return (obj as UserLastAction)?.Equals(this) == true;
            }

            public override int GetHashCode()
            {
                return UserId.GetHashCode() ^ ActionDateTime.GetHashCode();
            }
        }
    }
}
