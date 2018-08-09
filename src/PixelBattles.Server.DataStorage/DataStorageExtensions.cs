﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelBattles.Server.DataStorage.Stores;

namespace PixelBattles.Server.DataStorage
{
    public static class DataStorageExtensions
    {
        public static IServiceCollection AddDataStorage(this IServiceCollection services, IConfigurationRoot configuration)
        {
            return services
                .AddMongoDbStorage(configuration)
                .AddStores();
        }

        private static IServiceCollection AddStores(this IServiceCollection services)
        {
            return services
                .AddScoped<IBattleStore,BattleStore>();
        }
    }
}