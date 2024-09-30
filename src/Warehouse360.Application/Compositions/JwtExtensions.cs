using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Warehouse360.Application.IdentityManagement.Abstractions;
using Warehouse360.Application.IdentityManagement.Security;

namespace Warehouse360.Application.Compositions;

public static class JwtExtensions
{
    public static IServiceCollection AddJwtAuthenticationExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(config =>
        {
            config.Secret = configuration["JWT_SECRET"] 
                            ?? throw new ArgumentNullException("JWT_SECRET environment variable is missing.");
            config.Issuer = configuration["JWT_ISSUER"] 
                            ?? throw new ArgumentNullException("JWT_ISSUER environment variable is missing.");
            config.Audience = configuration["JWT_AUDIENCE"] 
                              ?? throw new ArgumentNullException("JWT_AUDIENCE environment variable is missing.");
        });

        services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // Внедряем IOptions<JwtSettings> через DI
                var sp = services.BuildServiceProvider();
                var jwtSettings = sp.GetRequiredService<IOptions<JwtSettings>>().Value;

                var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });


        services.AddAuthorization(options =>
        {
            // Политики на основе разрешений (Permissions)
            options.AddPolicy("ManageUsersPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("ManageUsers")));

            options.AddPolicy("ViewUsersPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("ViewUsers")));

            options.AddPolicy("ManageProductsPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("ManageProducts")));

            options.AddPolicy("ViewProductsPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("ViewProducts")));

            options.AddPolicy("ManageInventoryPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("ManageInventory")));

            options.AddPolicy("ViewInventoryPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("ViewInventory")));

            options.AddPolicy("ManageOrdersPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("ManageOrders")));

            options.AddPolicy("ViewOrdersPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("ViewOrders")));

            options.AddPolicy("ProcessOrdersPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("ProcessOrders")));

            options.AddPolicy("ManageCategoriesPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("ManageCategories")));

            options.AddPolicy("ViewCategoriesPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("ViewCategories")));

            // Политики на основе ролей
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
            options.AddPolicy("WarehouseWorkerPolicy", policy => policy.RequireRole("WarehouseWorker"));
            options.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
        });

        return services;
    }

    public static IApplicationBuilder UseJwtAuthenticationAndAuthorization(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}