using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public class UserActionCache : IUserActionCache
    {
        private ReaderWriterLockSlim collectionLock;
        
        private UserAction[] userActions;

        private int sizeLimit;
        public int SizeLimit
        {
            get
            {
                try
                {
                    collectionLock.EnterReadLock();
                    return userActions.Length;
                }
                finally
                {
                    collectionLock.ExitReadLock();
                }
            }
            set
            {
                try
                {
                    collectionLock.EnterWriteLock();
                    ResizeInternal(value);
                }
                finally
                {
                    collectionLock.EnterWriteLock();
                }
            }
        }

        private int minChangeIndex;
        public int MinChangeIndex
        {
            get
            {
                try
                {
                    collectionLock.EnterReadLock();
                    return minChangeIndex;
                }
                finally
                {
                    collectionLock.ExitReadLock();
                }
            }
        }

        private int maxChangeIndex;
        public int MaxChangeIndex
        {
            get
            {
                try
                {
                    collectionLock.EnterReadLock();
                    return maxChangeIndex;
                }
                finally
                {
                    collectionLock.ExitReadLock();
                }
            }
        }

        public UserActionCache(int sizeLimit)
        {
            if (sizeLimit < 1)
            {
                throw new ArgumentException("Size limit can't be 0 or less.");
            }

            this.sizeLimit = sizeLimit;
            this.userActions = new UserAction[sizeLimit];
            this.collectionLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        private void ResizeInternal(int size)
        {
            if (size >= userActions.Length)
            {
                Array.Resize(ref userActions, size);
            }
            else
            {
                var newArray = new UserAction[size];
                int lastIndex = maxChangeIndex - minChangeIndex;
                int indexCopyFrom = lastIndex - size + 1;
                Array.Copy(userActions, indexCopyFrom > 0 ? indexCopyFrom : 0, newArray, 0, size);
                minChangeIndex = userActions[0]?.ChangeIndex ?? 0;
            }
        }
                        
        public void Push(UserAction item)
        {
            try
            {
                collectionLock.EnterWriteLock();
                PushInternal(item);
            }
            finally
            {
                collectionLock.ExitWriteLock();
            }
        }

        private void PushInternal(UserAction item)
        {
            if (maxChangeIndex == 0)
            {
                userActions[0] = item;
                maxChangeIndex = item.ChangeIndex;
                minChangeIndex = item.ChangeIndex;
                return;
            }
            
            int newItemIndex = maxChangeIndex - minChangeIndex + 1;
            if (userActions.Length - newItemIndex == 0)
            {
                ShiftItemsInternal(1);
                minChangeIndex = userActions[0].ChangeIndex;
            }
            userActions[newItemIndex] = item;
            maxChangeIndex = item.ChangeIndex;
        }

        private void ShiftItemsInternal(int offset)
        {
            Array.Copy(userActions, offset, userActions, 0, userActions.Length - offset);
        }

        public void Push(ICollection<UserAction> items)
        {
            try
            {
                collectionLock.EnterWriteLock();
                PushInternal(items);
            }
            finally
            {
                collectionLock.ExitWriteLock();
            }
        }

        private void PushInternal(ICollection<UserAction> items)
        {
            if (items.Count >= userActions.Length)
            {
                Array.Copy(items.ToArray(), items.Count - userActions.Length, userActions, 0, userActions.Length);
                maxChangeIndex = userActions[userActions.Length - 1].ChangeIndex;
                minChangeIndex = userActions[0].ChangeIndex;
            }
            else
            {
                if (maxChangeIndex == 0)
                {
                    Array.Copy(items.ToArray(), 0, userActions, 0, items.Count);
                    maxChangeIndex = userActions[items.Count - 1].ChangeIndex;
                    minChangeIndex = userActions[0].ChangeIndex;
                }
                else
                {
                    int newItemIndex = maxChangeIndex - minChangeIndex + 1;
                    int requiredOffset = items.Count + newItemIndex - userActions.Length;
                    if (requiredOffset > 0)
                    {
                        ShiftItemsInternal(requiredOffset);
                        minChangeIndex = userActions[0].ChangeIndex;
                    }
                    Array.Copy(items.ToArray(), 0, userActions, newItemIndex - requiredOffset, items.Count);
                    maxChangeIndex = userActions[newItemIndex - requiredOffset + items.Count].ChangeIndex;
                }
            }
        }

        public void Clear()
        {
            try
            {
                collectionLock.EnterWriteLock();
                ClearInternal();
            }
            finally
            {
                collectionLock.ExitWriteLock();
            }
            
        }

        private void ClearInternal()
        {
            Array.Clear(userActions, 0, userActions.Length);
            minChangeIndex = 0;
            maxChangeIndex = 0;
        }
        
        public UserAction[] GetRange(int fromChangeIndex, int toChangeIndex)
        {
            try
            {
                collectionLock.EnterReadLock();
                return GetRangeInternal(fromChangeIndex, toChangeIndex);
            }
            finally
            {
                collectionLock.ExitReadLock();
            }
        }

        private UserAction[] GetRangeInternal(int fromChangeIndex, int toChangeIndex)
        {
            if (minChangeIndex > fromChangeIndex || maxChangeIndex < toChangeIndex)
            {
                return null;
            }
            else
            {
                int itemsToCopy = toChangeIndex - fromChangeIndex + 1;
                UserAction[] resultArray = new UserAction[itemsToCopy];
                Array.Copy(userActions, fromChangeIndex - minChangeIndex, resultArray, 0, itemsToCopy);
                return resultArray;
            }
        } 
    }
}
