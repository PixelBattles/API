using AutoMapper;
using PixelBattles.API.Server.BusinessLogic.Battles.Models;
using PixelBattles.API.Server.BusinessLogic.Images.Models;
using PixelBattles.API.Server.DataStorage.Stores.Battles;
using PixelBattles.API.Server.DataStorage.Stores.Images;
using System.Linq;
using System.Reflection;

namespace PixelBattles.API.Server.BusinessLogic
{
    public partial class BusinessLogicMappingProfile : Profile
    {
        public BusinessLogicMappingProfile()
        {
            this.GetType()
                .GetTypeInfo()
                .DeclaredMethods
                .Where(t => t.Name.Contains("Initialize"))
                .Where(t => t.IsPrivate)
                .Where(t => !t.IsStatic)
                .ToList()
                .ForEach(t => t.Invoke(this, null));
        }

        private void InitializeBattleSettings()
        {
            CreateMap<BattleSettingsEntity, BattleSettings>();
        }

        private void InitializeBattle()
        {
            CreateMap<BattleEntity, Battle>();
        }

        private void InitializeImage()
        {
            CreateMap<ImageEntity, Image>();
        }
    }
}