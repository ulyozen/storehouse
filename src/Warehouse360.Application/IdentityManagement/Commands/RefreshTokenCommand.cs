using MediatR;
using Warehouse360.Application.IdentityManagement.Dtos;

namespace Warehouse360.Application.IdentityManagement.Commands;

public class RefreshTokenCommand : IRequest<AuthResultDto>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public RefreshTokenCommand(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}