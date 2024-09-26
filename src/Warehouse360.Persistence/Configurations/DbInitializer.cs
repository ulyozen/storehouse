using Dapper;

namespace Warehouse360.Persistence.Configurations;

public class DbInitializer(IDbConnectionFactory connectionFactory)
{
    public async Task InitializeAsync()
    {
        using var connection = await connectionFactory.CreateConnectionAsync();
        
        const string users = 
            $"""
             CREATE TABLE IF NOT EXISTS Users (
                 Id UUID PRIMARY KEY,
                 Username VARCHAR(100) NOT NULL,
                 Email VARCHAR(255) NOT NULL,
                 PasswordHash VARCHAR(255) NOT NULL,
                 CreatedAt TIMESTAMPTZ NOT NULL DEFAULT NOW(),
                 UpdatedAt TIMESTAMPTZ NOT NULL DEFAULT NOW()
             );
             """;
        await connection.ExecuteAsync(users);
        
        const string usersIndexes = 
            $"""
             CREATE UNIQUE INDEX CONCURRENTLY IF NOT EXISTS idx_username 
             ON Users 
             using btree(Username);
             
             CREATE UNIQUE INDEX CONCURRENTLY IF NOT EXISTS idx_email 
             ON Users 
             using btree(Email);
             """;
        await connection.ExecuteAsync(usersIndexes);
        
        const string roles = 
            $"""
             CREATE TABLE IF NOT EXISTS Roles (
                 Id UUID PRIMARY KEY,
                 Name VARCHAR(100) NOT NULL
             );
             """;
        await connection.ExecuteAsync(roles);

        const string rolesIndexes =
            $"""
             CREATE UNIQUE INDEX IF NOT EXISTS idx_name
             ON Roles
             using btree(Name);
             """;
        await connection.ExecuteAsync(rolesIndexes);
        
        const string userRules = 
            $"""
             CREATE TABLE IF NOT EXISTS UserRoles (
                 UserId UUID NOT NULL,
                 RoleId UUID NOT NULL,
                 PRIMARY KEY (UserId, RoleId),
                 FOREIGN KEY (UserId) REFERENCES Users(Id),
                 FOREIGN KEY (RoleId) REFERENCES Roles(Id)
             );
             """;
        await connection.ExecuteAsync(userRules);
        
        const string permissions = 
            $"""
             CREATE TABLE IF NOT EXISTS Permissions (
                 Id UUID PRIMARY KEY,
                 Name VARCHAR(100) NOT NULL
             )
             """;
        await connection.ExecuteAsync(permissions);
        
        const string permissionsIndexes =
            $"""
             CREATE UNIQUE INDEX IF NOT EXISTS idx_name
             ON Permissions
             using btree(Name);
             """;
        await connection.ExecuteAsync(permissionsIndexes);
        
        const string rolePermissions = 
            $"""
             CREATE TABLE IF NOT EXISTS RolePermissions (
                 RoleId UUID NOT NULL,
                 PermissionId UUID NOT NULL,
                 PRIMARY KEY (RoleId, PermissionId),
                 FOREIGN KEY (RoleId) REFERENCES Roles(Id),
                 FOREIGN KEY (PermissionId) REFERENCES Permissions(Id)
             )
             """;
        await connection.ExecuteAsync(rolePermissions);
        
        const string refreshToken = 
            $"""
             CREATE TABLE IF NOT EXISTS RefreshTokens (
                 Id UUID PRIMARY KEY,
                 UserId UUID NOT NULL,
                 Token VARCHAR(255) NOT NULL,
                 ExpiresAt TIMESTAMPTZ NOT NULL,
                 CreatedAt TIMESTAMPTZ NOT NULL DEFAULT NOW(),
                 FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
             )
             """;
        await connection.ExecuteAsync(refreshToken);
    }
}