using System;

namespace PixelBattles.API.Server.BusinessLogic.BattleToken
{
    public class BattleTokenOptions
    {
        public BattleTokenOptions()
        {
            //Default values
            SecretKey = "This is default key for dev purposes";
            DefaultIssuer = "PixelBattlesServer";
            DefaultAudience = "PixelBattlesClient";
            DefaultTimeLife = TimeSpan.FromDays(1);
        }

        public string SecretKey { get; set; }

        public string DefaultIssuer { get; set; }

        public string DefaultAudience { get; set; }

        public TimeSpan DefaultTimeLife { get; set; }
    }
}