using System.Collections.Generic;

namespace PixelBattles.Server.BusinessLogic.Processors
{
    public interface IUserActionCache
    {
        int SizeLimit { get; set; }

        void Push(UserAction item);
        
        void Push(ICollection<UserAction> items);

        void Clear();

        ICollection<UserAction> GetRange(int fromChangeIndex, int toChangeIndex);
    }
}
