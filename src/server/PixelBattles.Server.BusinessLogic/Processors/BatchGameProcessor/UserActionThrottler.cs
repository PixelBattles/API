using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public sealed class UserActionThrottler
    {
        private TimeSpan timeSpan;

        public UserActionThrottler(TimeSpan timeSpan)
        {
            this.timeSpan = timeSpan;
            this.userActions = new ConcurrentDictionary<Guid, UserLastAction>();
        }
        
        private ConcurrentDictionary<Guid, UserLastAction> userActions;

        public bool CanUserContinue(Guid userId, DateTime actionDateTime)
        {
            var newUserAction = new UserLastAction(userId, actionDateTime);
            
            var resultUserAction = userActions.AddOrUpdate(userId, newUserAction, (key, currentLastAction) => currentLastAction.LastAction.Add(timeSpan) > actionDateTime ? currentLastAction : newUserAction);

            return ReferenceEquals(newUserAction, resultUserAction);
        }

        public void Clear(DateTime actionDateTime)
        {
            foreach (var userAction in userActions)
            {
                if (userAction.Value.LastAction.Add(timeSpan) < actionDateTime)
                {
                    ((ICollection<KeyValuePair<Guid, UserLastAction>>)userActions).Remove(userAction);
                }
            }
        }

        internal class UserLastAction : IEquatable<UserLastAction>
        {
            public Guid UserId { get; private set; }

            public DateTime LastAction { get; private set; }

            public UserLastAction(Guid userId, DateTime dateTime)
            {
                UserId = userId;
                LastAction = dateTime;
            }

            public bool Equals(UserLastAction userLastAction)
            {
                return UserId == userLastAction.UserId 
                    && LastAction == userLastAction.LastAction;
            }

            public override bool Equals(object obj)
            {
                return (obj as UserLastAction)?.Equals(this) == true;
            }

            public override int GetHashCode()
            {
                return UserId.GetHashCode() ^ LastAction.GetHashCode();
            }
        }
    }
}
