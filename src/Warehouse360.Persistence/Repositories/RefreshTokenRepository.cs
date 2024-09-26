using Dapper;
using Warehouse360.Core.IdentityManagement.Entities;
using Warehouse360.Core.IdentityManagement.Repositories;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Persistence.Repositories;

public class RefreshTokenRepository(IUnitOfWork unitOfWork) : IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        const string query = $"""SELECT * FROM RefreshTokens WHERE Token = @Token""";
        return await unitOfWork.Connection.QuerySingleOrDefaultAsync<RefreshToken>(query, new { Id = token });
    }

    public async Task<bool> AddAsync(RefreshToken refreshToken)
    {
        const string query = 
            $"""
             INSERT INTO RefreshTokens (Id, UserId, Token, ExpiresAt)
             VALUES (@Id, @UserId, @Token, @ExpiresAt)
             """;
        var parameters = new 
        { 
            refreshToken.Id,
            refreshToken.UserId,
            refreshToken.Token,
            refreshToken.ExpiresAt 
        };
        
        var command = new CommandDefinition(query, parameters);
        
        var result = await unitOfWork.Connection.ExecuteAsync(command);
        
        return result > 0;
    }

    public async Task<bool> RemoveAsync(Guid id)
    {
        const string query = $"""DELETE FROM RefreshTokens WHERE Id=@Id""";
        var result = await unitOfWork.Connection.ExecuteAsync(query, new { Id = id });

        return result > 0;
    }
}