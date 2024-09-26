using Warehouse360.Application.IdentityManagement.Dtos;

namespace Warehouse360.Application.IdentityManagement.Abstractions;

public interface IRoleCacheService
{
    Task<RoleDto> GetRoleAsync(Guid roleId);
    Task SetRoleAsync(RoleDto role, TimeSpan expiration);
    Task RemoveRoleAsync(Guid roleId);
}