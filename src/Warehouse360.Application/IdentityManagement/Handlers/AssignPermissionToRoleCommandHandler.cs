using MediatR;
using Warehouse360.Application.IdentityManagement.Commands;
using Warehouse360.Core.IdentityManagement.Repositories;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Application.IdentityManagement.Handlers;

public class AssignPermissionToRoleCommandHandler : IRequestHandler<AssignPermissionToRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    
    public AssignPermissionToRoleCommandHandler(
        IUnitOfWork unitOfWork, 
        IRoleRepository roleRepository, 
        IPermissionRepository permissionRepository)
    {
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
    }
    
    public async Task<bool> Handle(AssignPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var role = await _roleRepository.GetByIdAsync(request.RoleId);
            if (role is null)
                throw new ArgumentNullException($"Role with ID {request.RoleId} doesnt exist");
        
            var permission = await _permissionRepository.GetByIdAsync(request.PermissionId);
            if (permission is null)
                throw new ArgumentNullException($"Permission with ID {request.PermissionId} doesnt exist");
        
            role.AssignPermission(permission);
            
            var result = await _roleRepository.AssignPermissionToRoleAsync(role);
            await _unitOfWork.CommitTransactionAsync();

            return result;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
        
    }
}