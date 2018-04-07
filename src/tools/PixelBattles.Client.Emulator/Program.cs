using Microsoft.Extensions.Logging;
using System;

namespace PixelBattles.Client.Emulator
{
    class Program
    {
        private static bool running = true;

        static void Main(string[] args)
        {
            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddConsole();
            ILogger logger = loggerFactory.CreateLogger<Program>();

            string baseUrl = "http://localhost:10000/hub/test";
            HubClient emulatorClient = new HubClient(logger, baseUrl);
            while (running)
            {
                switch (Console.ReadLine())
                {
                    case "help":
                        PrintCommands();
                        break;
                    case "exit":
                        running = false;
                        break;
                    case "connect":
                        emulatorClient.ConnectAsync(Guid.Empty).ConfigureAwait(false).GetAwaiter().GetResult();
                        break;
                    case "disconnect":
                        emulatorClient.DisconnectAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                        break;
                    default:
                        break;
                }
                
            }
        }

        private static void PrintCommands()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("-help");
            Console.WriteLine("-exit");
            Console.WriteLine("-connect");
            Console.WriteLine("-disconnect");
        }
    }
}
