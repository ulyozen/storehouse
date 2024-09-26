using Warehouse360.Core.IdentityManagement.Entities;
using Warehouse360.Core.IdentityManagement.ValueObjects;

namespace Warehouse360.Core.IdentityManagement.Services;

public interface IUserService
{
    Task<User> CreateUser(string username, Email email, string password);
    Task AssignRole(User user, Role role);
    Task ChangePassword(Guid userId, string newPassword);
}