using System;

namespace PixelBattles.Server.IntegrationTests.Clients
{
    public class NameGenerator
    {
        private int index = 0;

        public NameGenerator()
        {

        }

        public string GenerateBattleName()
        {
            return "TEST_" + DateTime.UtcNow.ToString() + ++index;
        }
    }
}
