using MediatR;
using Warehouse360.Application.IdentityManagement.Commands;
using Warehouse360.Application.IdentityManagement.Dtos;
using Warehouse360.Core.IdentityManagement.Entities;
using Warehouse360.Core.IdentityManagement.Repositories;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Application.IdentityManagement.Handlers;

public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, PermissionDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPermissionRepository _permissionRepository;

    public CreatePermissionCommandHandler(IUnitOfWork unitOfWork, IPermissionRepository permissionRepository)
    {
        _unitOfWork = unitOfWork;
        _permissionRepository = permissionRepository;
    }
    
    public async Task<PermissionDto> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var permission = new Permission(Guid.NewGuid(), request.Name);
            await _permissionRepository.AddAsync(permission);
            await _unitOfWork.CommitTransactionAsync();
            
            return new PermissionDto(permission.Id, permission.Name);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
        
    }
}