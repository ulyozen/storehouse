using Microsoft.Extensions.DependencyInjection;
using Warehouse360.Application.CatalogManagement.Abstractions;
using Warehouse360.Redis.Services;

namespace Warehouse360.Redis.Extensions;

public static class ServiceCollection
{
    public static IServiceCollection AddRedisExtension(this IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
            options.InstanceName = "SampleInstance";
        });

        services.AddScoped<ICategoryCacheService, CategoryCacheService>();
        
        return services;
    }
}