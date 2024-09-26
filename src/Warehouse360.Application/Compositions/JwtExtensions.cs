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
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    ValidateLifetime = true
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOrManagerPolicy", policy => 
                policy.RequireAssertion(context => 
                    context.User.IsInRole("Admin") || context.User.IsInRole("Manager")));
            
            options.AddPolicy("ManageOrdersPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("ManageOrders")));
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