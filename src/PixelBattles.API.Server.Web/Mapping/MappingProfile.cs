using AutoMapper;
using PixelBattles.API.DataTransfer;
using PixelBattles.API.DataTransfer.Battles;
using PixelBattles.API.DataTransfer.Images;
using PixelBattles.API.Server.BusinessLogic.Battles.Models;
using PixelBattles.API.Server.BusinessLogic.Images.Models;
using System.Linq;
using System.Reflection;

namespace PixelBattles.API.Server.Web.Mapping
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
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

        private void Initialize()
        {
            CreateMap<Result, ResultDTO>();
        }

        private void InitializeBattles()
        {
            CreateMap<Battle, BattleDTO>();
            CreateMap<BattleSettings, BattleSettingsDTO>();
            CreateMap<BattleFilter, BattleFilterDTO>();
            CreateMap<CreateBattleResult, CreateBattleResultDTO>();
            CreateMap<CreateBattleTokenResult, CreateBattleTokenResultDTO>();
        }

        private void InitializeImages()
        {
            CreateMap<Image, ImageDTO>();
            CreateMap<CreateImageResult, CreateImageResultDTO>();
        }
    }
}