using Warehouse360.Core.IdentityManagement.Entities;

namespace Warehouse360.Core.IdentityManagement.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> LoginByUsernameAsync(string username);
    Task<bool> AddAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<bool> AssignRoleToUserAsync(User user);
    Task<bool> DeleteAsync(User user);
}