using Dapper;
using Warehouse360.Core.IdentityManagement.Entities;
using Warehouse360.Core.IdentityManagement.Repositories;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Persistence.Repositories;

public class RoleRepository(IUnitOfWork unitOfWork) : IRoleRepository
{
    public async Task<Role?> GetByIdAsync(Guid id)
    {
        const string query = $"""SELECT * FROM Roles WHERE Id = @Id""";
        return await unitOfWork.Connection.QuerySingleOrDefaultAsync<Role>(query, new { Id = id });
    }
    
    public async Task<List<Role>> GetAllAsync()
    {
        const string query = $"""SELECT * FROM Roles""";
        return (await unitOfWork.Connection.QueryAsync<Role>(query)).ToList();
    }
    
    public async Task<bool> AddAsync(Role role)
    {
        const string query = 
            $"""
             INSERT INTO Roles (Id, Name)
             VALUES (@Id, @Name)
             """;
        var parameters = new { Id = role.Id, Name = role.Name };
        
        var command = new CommandDefinition(query, parameters);
        
        var result = await unitOfWork.Connection.ExecuteAsync(command);
        
        return result > 0;
    }
    
    public async Task<bool> UpdateAsync(Role role)
    {
        const string query = 
            $"""
             Update Roles Set Name = @Name
             WHERE Id = @Id
             """;
        var parameters = new { role.Name, role.Id };
        
        var command = new CommandDefinition(query, parameters);
        
        var result = await unitOfWork.Connection.ExecuteAsync(command);
        
        return result > 0;
    }
    
    public async Task<bool> AssignPermissionToRoleAsync(Role role)
    {
        const string query = 
            $"""
             INSERT INTO RolePermissions (RoleId, PermissionId)
             VALUES (@RoleId, @PermissionId)
             """;

        var parameters = role.Permissions.Select(permission => new 
        {
            RoleId = role.Id, 
            PermissionId = permission.Id
        });

        var result = await unitOfWork.Connection.ExecuteAsync(query, parameters);

        return result > 0;
    }
}