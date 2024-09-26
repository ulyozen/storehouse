using MediatR;

namespace Warehouse360.Application.IdentityManagement.Commands;

public class UpdateUserCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public UpdateUserCommand(Guid userId, string username, string email, string password)
    {
        UserId = userId;
        Username = username;
        Email = email;
        Password = password;
    }
}