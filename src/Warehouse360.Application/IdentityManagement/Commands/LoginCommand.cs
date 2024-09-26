using MediatR;
using Warehouse360.Application.IdentityManagement.Dtos;

namespace Warehouse360.Application.IdentityManagement.Commands;

public class LoginCommand : IRequest<AuthResultDto>
{
    public string Username { get; set; }
    public string Password { get; set; }

    public LoginCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }
}