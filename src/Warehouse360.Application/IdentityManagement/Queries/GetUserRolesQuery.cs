using MediatR;
using Warehouse360.Application.IdentityManagement.Dtos;

namespace Warehouse360.Application.IdentityManagement.Queries;

public class GetUserRolesQuery : IRequest<IEnumerable<RoleDto>>
{
    public Guid UserId { get; set; }

    public GetUserRolesQuery(Guid userId)
    {
        UserId = userId;
    }
}