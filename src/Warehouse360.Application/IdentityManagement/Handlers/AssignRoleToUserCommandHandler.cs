using MediatR;
using Warehouse360.Application.IdentityManagement.Commands;
using Warehouse360.Core.IdentityManagement.Repositories;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Application.IdentityManagement.Handlers;

public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public AssignRoleToUserCommandHandler(
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository, 
        IRoleRepository roleRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<bool> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                throw new ArgumentNullException($"User with ID {request.UserId} doesnt exist");
        
            var role = await _roleRepository.GetByIdAsync(request.RoleId);
            if (role is null)
                throw new ArgumentNullException($"User with ID {request.RoleId} doesnt exist");

            user.AssignRole(role);
            
            var result = await _userRepository.AssignRoleToUserAsync(user);
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