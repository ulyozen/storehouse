using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Warehouse360.Application.Extensions;

public static class ServiceCollection
{
    public static IServiceCollection AddApplicationExtension(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}