using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse360.Application.CatalogManagement.Abstractions;
using Warehouse360.Application.IdentityManagement.Abstractions;
using Warehouse360.Redis.Services;

namespace Warehouse360.Redis.Compositions;

public static class ServiceCollection
{
    public static IServiceCollection AddRedisExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "RedisInstance";
        });

        services
            .AddScoped<ICategoryCacheService, CategoryCacheService>()
            .AddScoped<IUserCacheService, UserCacheService>()
            .AddScoped<IRoleCacheService, RoleCacheService>();
        
        return services;
    }
}