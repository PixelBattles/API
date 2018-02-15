using SixLabors.ImageSharp;
using System;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class UserAction
    {
        public Guid UserActionId { get; set; }

        public Guid GameId { get; set; }

        public int ChangeIndex { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public Rgba32 Pixel { get; set; }
    }

    public partial class BusinessLogicMappingProfile
    {
        private void InitializeUserAction()
        {
        }
    }
}
