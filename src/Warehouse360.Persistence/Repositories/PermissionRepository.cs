using Dapper;
using Warehouse360.Core.IdentityManagement.Entities;
using Warehouse360.Core.IdentityManagement.Repositories;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Persistence.Repositories;

public class PermissionRepository(IUnitOfWork unitOfWork) : IPermissionRepository
{
    public async Task<Permission?> GetByIdAsync(Guid id)
    {
        const string query = $"""SELECT * FROM Permissions WHERE Id = @Id""";
        return await unitOfWork.Connection.QuerySingleOrDefaultAsync<Permission>(query, new { Id = id });
    }
    
    public async Task<List<Permission>> GetAllAsync()
    {
        const string query = $"""SELECT * FROM Permissions""";
        return (await unitOfWork.Connection.QueryAsync<Permission>(query)).ToList();
    }

    public async Task<bool> AddAsync(Permission permission)
    {
        const string query = 
            $"""
             INSERT INTO Permissions (Id, Name)
             VALUES (@Id, @Name)
             """;
        var parameters = new { Id = permission.Id, Name = permission.Name };
        
        var command = new CommandDefinition(query, parameters);
        
        var result = await unitOfWork.Connection.ExecuteAsync(command);
        
        return result > 0;
    }

    public async Task<bool> UpdateAsync(Permission permission)
    {
        const string query = 
            $"""
             Update Permissions Set Name = @Name
             WHERE Id = @Id
             """;
        var parameters = new { Name = permission.Name, Id = permission.Id };
        
        var command = new CommandDefinition(query, parameters);
        
        var result = await unitOfWork.Connection.ExecuteAsync(command);
        
        return result > 0;
    }
}