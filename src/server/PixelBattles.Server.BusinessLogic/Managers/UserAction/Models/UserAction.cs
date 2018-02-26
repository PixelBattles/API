using SixLabors.ImageSharp;
using System;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class UserAction
    {
        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

        public int ChangeIndex { get; set; }

        public int YIndex { get; set; }

        public int XIndex { get; set; }

        public Rgba32 Pixel { get; set; }
    }

    public partial class BusinessLogicMappingProfile
    {
        private void InitializeUserAction()
        {
        }
    }
}
