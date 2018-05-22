using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PixelBattles.Server.DataStorage;

namespace PixelBattles.Server.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://0.0.0.0:5000")
                .Build();
    }
}
