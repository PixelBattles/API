using System;
using System.Threading;

namespace PixelBattles.Server.IntegrationTests.Clients
{
    public class NameGenerator
    {
        private static int index = 0;

        public NameGenerator()
        {

        }

        public string GenerateBattleName()
        {
            return "TEST_" + DateTime.UtcNow.ToString() + Interlocked.Increment(ref index);
        }

        public string GenerateGameName()
        {
            return "TEST_" + DateTime.UtcNow.ToString() + Interlocked.Increment(ref index);
        }
    }
}
