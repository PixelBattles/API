using PixelBattles.Server.DataStorage.Models;

namespace PixelBattles.Server.BusinessLogic.Models
{
    public class BattleSettings
    {
        public int ChunkHeight { get; set; }

        public int ChunkWidth { get; set; }

        public int MaxHeightIndex { get; set; }

        public int MaxWidthIndex { get; set; }

        public int MinHeightIndex { get; set; }

        public int MinWidthIndex { get; set; }

        public int CenterX { get; set; }

        public int CenterY { get; set; }

        public int Cooldown { get; set; }
    }

    public partial class BusinessLogicMappingProfile
    {
        private void InitializeBattleSettings()
        {
            CreateMap<BattleSettingsEntity, BattleSettings>();
        }
    }
}
