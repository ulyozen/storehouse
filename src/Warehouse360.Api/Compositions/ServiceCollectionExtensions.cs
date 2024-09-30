using Warehouse360.Application.Compositions;
using Warehouse360.Application.IdentityManagement.Security;

namespace Warehouse360.Api.Compositions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtSettings(this IServiceCollection services, IConfiguration configuration)
    {
        Console.WriteLine("JWT_SECRET:" + configuration["JWT_SECRET"]);
        Console.WriteLine("JWT_ISSUER:" + configuration["JWT_ISSUER"]);
        Console.WriteLine("JWT_AUDIENCE:" + configuration["JWT_AUDIENCE"]);
        
        
        

        return services;
    }
}