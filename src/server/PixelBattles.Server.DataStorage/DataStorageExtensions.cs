using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PixelBattles.Server.DataStorage
{
    public static class DataStorageExtensions
    {
        public static IServiceCollection AddDataStorage(this IServiceCollection services, IConfigurationRoot configuration)
        {
            return services
                .AddDbContext<PixelBattlesDbContext>(options =>
                {
                    options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"],
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(DataStorageExtensions).Assembly.GetName().Name));
                });
        }

        public static IWebHost Migrate(this IWebHost webhost)
        {
            using (var scope = webhost.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<PixelBattlesDbContext>())
                {
                    dbContext.Database.Migrate();
                    dbContext.EnsureSeedData();
                }
            }
            return webhost;
        }
    }
}
