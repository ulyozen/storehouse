using Warehouse360.Core.IdentityManagement.Entities;

namespace Warehouse360.Core.IdentityManagement.Repositories;

public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(Guid id);
    Task<List<Permission>> GetAllAsync();
    Task<bool> AddAsync(Permission permission);
    Task<bool> UpdateAsync(Permission permission);
    
}