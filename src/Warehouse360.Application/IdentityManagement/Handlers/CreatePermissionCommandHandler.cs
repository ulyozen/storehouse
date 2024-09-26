using MediatR;
using Warehouse360.Application.IdentityManagement.Commands;
using Warehouse360.Application.IdentityManagement.Dtos;
using Warehouse360.Core.IdentityManagement.Entities;
using Warehouse360.Core.IdentityManagement.Repositories;

namespace Warehouse360.Application.IdentityManagement.Handlers;

public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, PermissionDto>
{
    private readonly IPermissionRepository _permissionRepository;

    public CreatePermissionCommandHandler(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }
    
    public async Task<PermissionDto> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = new Permission(request.Name);
        await _permissionRepository.AddAsync(permission);

        return new PermissionDto(permission.Id, permission.Name);
    }
}