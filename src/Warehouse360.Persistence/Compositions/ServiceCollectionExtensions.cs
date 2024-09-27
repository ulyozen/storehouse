using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse360.Core.CatalogManagement.Repositories;
using Warehouse360.Core.IdentityManagement.Repositories;
using Warehouse360.Core.SeedWork.Interfaces;
using Warehouse360.Persistence.Configurations;
using Warehouse360.Persistence.Repositories;
using Warehouse360.Persistence.TypeHandlers;

namespace Warehouse360.Persistence.Compositions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceExtension(this IServiceCollection services, IConfiguration configuration)
    {
        SqlMapper.AddTypeHandler(new EmailTypeHandler());
        
        services
            .AddSingleton<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(configuration.GetConnectionString("Postgres")!))
            .AddSingleton<DbInitializer>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IPermissionRepository, PermissionRepository>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}