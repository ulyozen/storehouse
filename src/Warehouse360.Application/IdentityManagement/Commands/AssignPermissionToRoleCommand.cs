using MediatR;

namespace Warehouse360.Application.IdentityManagement.Commands;

public class AssignPermissionToRoleCommand : IRequest
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }

    public AssignPermissionToRoleCommand(Guid roleId, Guid permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }
}