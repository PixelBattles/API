using System;

namespace PixelBattles.Shared.DataTransfer.Hub
{
    public class ProcessActionDTO
    {
        public Guid GameId { get; set; }

        public int YIndex { get; set; }

        public int XIndex { get; set; }

        public byte[] Pixel { get; set; }
    }
}
