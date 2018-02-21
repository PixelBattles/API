using System.Collections.Generic;
using System.Threading;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public class UserActionCache
    {
        private ReaderWriterLockSlim collectionLock;

        private int cacheMaxSize;

        private SortedList<int, UserAction> userActions;

        public UserActionCache(int cacheMaxSize)
        {
            this.cacheMaxSize = cacheMaxSize;
            this.userActions = new SortedList<int, UserAction>(cacheMaxSize);
            this.collectionLock = new ReaderWriterLockSlim();
        }

        public void Push(IEnumerable<UserAction> newUserActions)
        {
            try
            {
                collectionLock.EnterWriteLock();

                foreach (var userAction in newUserActions)
                {
                    AddUserActionInternal(userAction);
                }
            }
            finally
            {
                collectionLock.ExitWriteLock();
            }
        }

        private void AddUserActionInternal(UserAction userAction)
        {
        }

        private void PrepareCollectionSize(int insertSize)
        {

        }

        public IEnumerable<UserAction> ResolveUserActions(int fromChangeIndex, int toChangeIndex)
        {
            return null;
        }
    }
}
