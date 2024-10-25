using MediatR;
using Warehouse360.Application.IdentityManagement.Dtos;

namespace Warehouse360.Application.IdentityManagement.Commands;

public class CreatePermissionCommand : IRequest<PermissionDto>
{
    public string Name { get; set; }

    public CreatePermissionCommand(string name)
    {
        Name = name;
    }
}