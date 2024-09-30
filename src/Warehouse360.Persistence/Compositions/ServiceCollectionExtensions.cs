using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Warehouse360.Application.Compositions;
using Warehouse360.Core.CatalogManagement.Repositories;
using Warehouse360.Core.IdentityManagement.Repositories;
using Warehouse360.Core.SeedWork.Interfaces;
using Warehouse360.Persistence.Configurations;
using Warehouse360.Persistence.Repositories;
using Warehouse360.Persistence.TypeHandlers;

namespace Warehouse360.Persistence.Compositions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceExtension(this IServiceCollection services)
    {
        SqlMapper.AddTypeHandler(new EmailTypeHandler());
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IPermissionRepository, PermissionRepository>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddDatabaseSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IDbConnectionFactory>(options =>
            {
                var dbSettings = options.GetRequiredService<IOptions<DatabaseSettings>>().Value;

                var connectionString = $"User ID={dbSettings.UserID};Password={dbSettings.Password};Host={dbSettings.Host};Port={dbSettings.Port};Database={dbSettings.Database};";
                
                return new NpgsqlConnectionFactory(connectionString);
            })
            .AddSingleton<DbInitializer>();
        
        services.Configure<DatabaseSettings>(config =>
        {
            config.UserID = configuration["POSTGRES_USER"] 
                            ?? throw new ArgumentNullException("POSTGRES_USER environment variable is missing.");
            config.Password = configuration["POSTGRES_PASSWORD"] 
                              ?? throw new ArgumentNullException("POSTGRES_PASSWORD environment variable is missing.");
            config.Host = configuration["POSTGRES_HOST"] ?? "localhost"; 

            if (!int.TryParse(configuration["POSTGRES_PORT"], out int port))
            {
                throw new ArgumentException("Invalid POSTGRES_PORT environment variable. Must be a valid integer.");
            }
            config.Port = port;
            config.Database = configuration["POSTGRES_DB_WAREHOUSE360"] 
                              ?? throw new ArgumentNullException("POSTGRES_DB_WAREHOUSE360 environment variable is missing.");;
        });

        return services;
    }
}