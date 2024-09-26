using MediatR;
using Warehouse360.Application.IdentityManagement.Dtos;

namespace Warehouse360.Application.IdentityManagement.Commands;

public class CreateUserCommand : IRequest<UserDto>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public CreateUserCommand(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }
}