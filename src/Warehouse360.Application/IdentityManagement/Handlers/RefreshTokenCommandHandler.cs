using MediatR;
using Warehouse360.Application.IdentityManagement.Abstractions;
using Warehouse360.Application.IdentityManagement.Commands;
using Warehouse360.Application.IdentityManagement.Dtos;
using Warehouse360.Core.IdentityManagement.Entities;
using Warehouse360.Core.IdentityManagement.Repositories;

namespace Warehouse360.Application.IdentityManagement.Handlers;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResultDto>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;

    public RefreshTokenCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
    }
    
    public async Task<AuthResultDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);
        if (refreshToken == null || refreshToken.IsExpired)
        {
            throw new ArgumentNullException("Invalid or expired refresh token: " + refreshToken);
        }

        var user = await _userRepository.GetByIdAsync(refreshToken.UserId);
        if (user is null)
        {
            throw new ArgumentNullException("User not found: " + user);
        }

        var newAccessToken = _jwtTokenGenerator.GenerateToken(user);
        var newRefreshToken = new RefreshToken(Guid.NewGuid(), user.Id, Guid.NewGuid().ToString(), DateTime.UtcNow.AddDays(7));
        await _refreshTokenRepository.AddAsync(newRefreshToken);

        await _refreshTokenRepository.RemoveAsync(refreshToken.Id);

        return new AuthResultDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token
        };
    }
}