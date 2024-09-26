using MediatR;
using Warehouse360.Application.IdentityManagement.Dtos;

namespace Warehouse360.Application.IdentityManagement.Queries;

public class GetUserByIdQuery : IRequest<UserDto>
{
    public Guid UserId { get; set; }

    public GetUserByIdQuery(Guid userId)
    {
        UserId = userId;
    }
}