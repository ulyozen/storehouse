using Warehouse360.Application.IdentityManagement.Abstractions;
using Warehouse360.Application.IdentityManagement.Dtos;

namespace Warehouse360.Redis.Services;

public class RoleCacheService : IRoleCacheService
{
    public Task<RoleDto> GetRoleAsync(Guid roleId)
    {
        throw new NotImplementedException();
    }

    public Task SetRoleAsync(RoleDto role, TimeSpan expiration)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRoleAsync(Guid roleId)
    {
        throw new NotImplementedException();
    }
}