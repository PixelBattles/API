using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PixelBattles.Server.BusinessLogic.Managers;
using PixelBattles.Server.BusinessLogic.Models;
using PixelBattles.Shared.DataTransfer.Api.Battle;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PixelBattles.Server.Web.Controllers.Api
{
    [Route("api")]
    public class BattleController : BaseApiController
    {
        protected IBattleManager BattleManager { get; set; }

        public BattleController(
            IBattleManager battleManager,
            IMapper mapper,
            ILogger<BattleController> logger) : base(
                mapper: mapper,
                logger: logger)
        {
            BattleManager = battleManager ?? throw new ArgumentNullException(nameof(battleManager));
        }
        
        [HttpGet("battle/{battleId:guid}")]
        public async Task<IActionResult> GetBattleAsync(Guid battleId)
        {
            try
            {
                var battle = await BattleManager.GetBattleAsync(battleId);
                if (battle == null)
                {
                    return NotFound();
                }
                var result = Mapper.Map<Battle, BattleDTO>(battle);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Exception(exception, "Error while getting battles.");
            }
        }

        #warning This method is test only and should be removed
        [HttpGet("battle/imageTest")]
        public async Task<IActionResult> GetBattleImageTestAsync()
        {
            try
            {
                using (Image<Rgb24> image = new Image<Rgb24>(400, 400))
                {
                    for (int i = 0; i < image.Height; i++)
                    {
                        for (int j = 0; j < image.Width; j++)
                        {
                            byte value = ((i + j) % 2) == 0 ? (byte)255 : (byte)0;
                            image[i, j] = new Rgb24(value, value, value);
                        }
                    }
                    Stream imageStream = new MemoryStream();
                    image.SaveAsPng(imageStream);
                    imageStream.Seek(0, SeekOrigin.Begin);
                    return File(imageStream, "image/png");
                }
            }
            catch (Exception exception)
            {
                return Exception(exception, "Error while getting battle image.");
            }
        }
    }
}
