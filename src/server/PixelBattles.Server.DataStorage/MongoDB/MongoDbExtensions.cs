using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PixelBattles.Server.DataStorage.Models;

namespace PixelBattles.Server.DataStorage
{
    public static class MongoDbExtensions
    {
        public static IServiceCollection AddMongoDbStorage(this IServiceCollection services, IConfigurationRoot configuration)
        {
            return services
                .Configure<MongoDbOptions>(configuration.GetSection(nameof(MongoDbOptions)))
                .AddScoped<IMongoClient>(s => new MongoClient(s.GetRequiredService<IOptions<MongoDbOptions>>().Value.ConnectionString))
                .AddScoped<IMongoDatabase>(s => s.GetRequiredService<IMongoClient>().GetDatabase(s.GetRequiredService<IOptions<MongoDbOptions>>().Value.Database))
                .AddMongoDbCollections();
        }
        
        private static IServiceCollection AddMongoDbCollections(this IServiceCollection services)
        {
            return services
                .AddScoped<IMongoCollection<BattleEntity>>(s => s.GetRequiredService<IMongoDatabase>().GetCollection<BattleEntity>(nameof(BattleEntity)));
        }
    }
}
