using System.Collections.Generic;

namespace PixelBattles.Shared.DataTransfer
{
    public class ResultDTO
    {
        public bool Succeeded { get; protected set; }

        public IEnumerable<ErrorDTO> Errors { get; protected set; }
    }
}
