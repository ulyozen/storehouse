using MediatR;
using Warehouse360.Application.IdentityManagement.Commands;
using Warehouse360.Core.IdentityManagement.Repositories;

namespace Warehouse360.Application.IdentityManagement.Handlers;

public class AssignPermissionToRoleCommandHandler : IRequestHandler<AssignPermissionToRoleCommand>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    
    public AssignPermissionToRoleCommandHandler(IRoleRepository roleRepository, IPermissionRepository permissionRepository)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
    }
    
    public async Task Handle(AssignPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.RoleId);
        if (role is null)
            throw new Exception($"Role with ID {request.RoleId} doesnt exist");
        
        var permission = await _permissionRepository.GetByIdAsync(request.PermissionId);
        if (permission is null)
            throw new Exception($"Permission with ID {request.PermissionId} doesnt exist");
        
        role.AssignPermission(permission);
        await _roleRepository.UpdateAsync(role);
    }
}