using Warehouse360.Application.IdentityManagement.Dtos;
using Warehouse360.Core.IdentityManagement.Entities;

namespace Warehouse360.Application.IdentityManagement.Abstractions;

public interface IUserCacheService
{
    Task<UserDto?> GetUserByIdAsync(Guid userId);
    Task<User?> GetUserByNameAsync(string username);
    Task SetUserByIdAsync(UserDto user, TimeSpan expiration);
    Task SetUserByNameAsync(User user, TimeSpan expiration);
    Task RemoveUserByIdAsync(Guid userId);
    Task RemoveUserByNameAsync(string username);
}