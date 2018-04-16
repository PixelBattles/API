using System.Collections.Generic;

namespace PixelBattles.Shared.DataTransfer
{
    public class ResultDTO
    {
        public bool Succeeded { get; set; }

        public IEnumerable<ErrorDTO> Errors { get; set; }
    }
}
