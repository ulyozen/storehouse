using Warehouse360.Core.IdentityManagement.Entities;

namespace Warehouse360.Core.IdentityManagement.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<bool> AddAsync(RefreshToken refreshToken);
    Task<bool> RemoveAsync(Guid id);
}