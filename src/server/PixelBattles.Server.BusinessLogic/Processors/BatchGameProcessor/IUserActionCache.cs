using PixelBattles.Server.BusinessLogic.Models;
using System;
using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public interface IUserActionCache : IDisposable
    {
        int SizeLimit { get; set; }

        void Push(UserAction item);
        
        void Push(ICollection<UserAction> items);

        void Clear();

        UserAction[] GetRange(int fromChangeIndex, int toChangeIndex);
    }
}
