using AutoMapper;
using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Shared.DataTransfer.Api.Battle;
using PixelBattles.Shared.DataTransfer.Api.Game;
using PixelBattles.Shared.DataTransfer.Api.Hub;
using System.Linq;
using System.Reflection;

namespace PixelBattles.Server.Web.Mapping
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

        private void InitializeOrganization()
        {
            CreateMap<Battle, BattleDTO>();
            CreateMap<BattleFilter, BattleFilterDTO>();
            CreateMap<CreateBattleResult, CreateBattleResultDTO>();
        }

        private void InitializeHub()
        {
            CreateMap<Hub, HubDTO>();
            CreateMap<CreateHubResult, CreateHubResultDTO>();
        }

        private void InitializeGame()
        {
            CreateMap<Game, GameDTO>();
            CreateMap<CreateGameResult, CreateGameResultDTO>();
            CreateMap<CreateGameTokenResult, CreateGameTokenResultDTO>();
        }
    }
}
