using MediatR;
using Warehouse360.Application.IdentityManagement.Dtos;

namespace Warehouse360.Application.IdentityManagement.Queries;

public class GetRolePermissionsQuery : IRequest<IEnumerable<PermissionDto>>
{
    public Guid RoleId { get; set; }

    public GetRolePermissionsQuery(Guid roleId)
    {
        RoleId = roleId;
    }
}