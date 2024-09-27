using Warehouse360.Core.IdentityManagement.Entities;

namespace Warehouse360.Core.IdentityManagement.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id);
    Task<List<Role>> GetAllAsync();
    Task<bool> AddAsync(Role role);
    Task<bool> UpdateAsync(Role role);
    Task<bool> AssignPermissionToRoleAsync(Role role);
}