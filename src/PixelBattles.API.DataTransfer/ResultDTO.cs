using System.Collections.Generic;

namespace PixelBattles.API.DataTransfer
{
    public class ResultDTO
    {
        public bool Succeeded { get; set; }

        public IEnumerable<ErrorDTO> Errors { get; set; }
    }
}
