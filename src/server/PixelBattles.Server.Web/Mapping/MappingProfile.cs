using AutoMapper;
using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Shared.DataTransfer.Api.Battle;
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
        }
    }
}
