using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Warehouse360.Application.IdentityManagement.Services;
using Warehouse360.Core.IdentityManagement.Services;

namespace Warehouse360.Application.Compositions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationExtension(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddValidatorsFromAssemblyContaining<IValidatorMarker>();

        services
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<IUserService, UserService>();

        return services;
    }
}