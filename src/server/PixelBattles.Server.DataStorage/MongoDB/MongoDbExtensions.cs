using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PixelBattles.Server.DataStorage.Models;

namespace PixelBattles.Server.DataStorage
{
    public static class MongoDbExtensions
    {
        public static IServiceCollection AddMongoDbStorage(this IServiceCollection services, IConfigurationRoot configuration)
        {
            return services
                .AddSingleton<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>())
                .AddScoped<IMongoClient>(s => new MongoClient(s.GetRequiredService<MongoDbSettings>().ConnectionString))
                .AddScoped<IMongoDatabase>(s => s.GetRequiredService<IMongoClient>().GetDatabase(s.GetRequiredService<MongoDbSettings>().Database))
                .AddMongoDbCollections();
        }

        private static IServiceCollection AddMongoDbCollections(this IServiceCollection services)
        {
            return services
                .AddScoped<IMongoCollection<BattleEntity>>(s => s.GetRequiredService<IMongoDatabase>().GetCollection<BattleEntity>(nameof(BattleEntity)));

        }
    }
}
