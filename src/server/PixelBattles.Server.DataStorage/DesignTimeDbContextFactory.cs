using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PixelBattles.Server.DataStorage
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PixelBattlesDbContext>
    {
        public PixelBattlesDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Custom.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<PixelBattlesDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new PixelBattlesDbContext(builder.Options);
        }
    }
}
