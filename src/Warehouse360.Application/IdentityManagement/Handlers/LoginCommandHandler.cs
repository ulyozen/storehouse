using MediatR;
using Warehouse360.Application.IdentityManagement.Abstractions;
using Warehouse360.Application.IdentityManagement.Commands;
using Warehouse360.Application.IdentityManagement.Dtos;
using Warehouse360.Core.IdentityManagement.Entities;
using Warehouse360.Core.IdentityManagement.Repositories;
using Warehouse360.Core.IdentityManagement.Services;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Application.IdentityManagement.Handlers;

public class LoginCommandHandler  : IRequestHandler<LoginCommand, AuthResultDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LoginCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher, 
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
    }
    
    public async Task<AuthResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var user = await _userRepository.LoginByUsernameAsync(request.Username);
            
            if (user is null || !_passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
            {
                throw new Exception($"User with username {request.Username} doesnt exist");
            };

            var jwtToken = _jwtTokenGenerator.GenerateToken(user);

            var refreshToken = new RefreshToken(
                Guid.NewGuid(), 
                user.Id, 
                Guid.NewGuid().ToString(),
                DateTime.UtcNow.AddDays(7));
        
            await _refreshTokenRepository.AddAsync(refreshToken);
            await _unitOfWork.CommitTransactionAsync();
             
            return new AuthResultDto
            {
                AccessToken = jwtToken,
                RefreshToken = refreshToken.Token
            };
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
        
    }
}