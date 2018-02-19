namespace PixelBattles.Shared.DataTransfer.Hub
{
    public class PendingActionDTO
    {
        public int YIndex { get; set; }

        public int XIndex { get; set; }

        public byte[] Pixel { get; set; }
    }
}
