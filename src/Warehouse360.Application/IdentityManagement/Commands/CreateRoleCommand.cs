using MediatR;
using Warehouse360.Application.IdentityManagement.Dtos;

namespace Warehouse360.Application.IdentityManagement.Commands;

public class CreateRoleCommand : IRequest<RoleDto>
{
    public string Name { get; set; }

    public CreateRoleCommand(string name)
    {
        Name = name;
    }
}