using MediatR;

namespace Warehouse360.Application.IdentityManagement.Commands;

public class AssignRoleToUserCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public AssignRoleToUserCommand(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}