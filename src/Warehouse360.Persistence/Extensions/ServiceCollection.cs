using Microsoft.Extensions.DependencyInjection;
using Warehouse360.Core.CatalogManagement.Repositories;
using Warehouse360.Persistence.Repositories;

namespace Warehouse360.Persistence.Extensions;

public static class ServiceCollection
{
    public static IServiceCollection AddPersistenceExtension(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}