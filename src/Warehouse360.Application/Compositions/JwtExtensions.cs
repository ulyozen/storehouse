using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Warehouse360.Application.IdentityManagement.Abstractions;
using Warehouse360.Application.IdentityManagement.Security;

namespace Warehouse360.Application.Compositions;

public static class JwtExtensions
{
    public static IServiceCollection AddJwtAuthenticationExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!);

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
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