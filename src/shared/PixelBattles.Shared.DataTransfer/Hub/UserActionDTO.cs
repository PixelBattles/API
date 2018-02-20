namespace PixelBattles.Shared.DataTransfer.Hub
{
    public class UserActionDTO
    {
        public int ChangeIndex { get; set; }

        public int YIndex { get; set; }

        public int XIndex { get; set; }

        public byte[] Pixel { get; set; }
    }
}
