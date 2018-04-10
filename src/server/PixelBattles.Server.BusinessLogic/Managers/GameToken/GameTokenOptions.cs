using System;

namespace PixelBattles.Server.BusinessLogic.Managers
{
    public class GameTokenOptions
    {
        public GameTokenOptions()
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