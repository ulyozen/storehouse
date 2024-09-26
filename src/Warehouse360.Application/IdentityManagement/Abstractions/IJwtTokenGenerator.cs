using Warehouse360.Core.IdentityManagement.Entities;

namespace Warehouse360.Application.IdentityManagement.Abstractions;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}