using MediatR;
using Warehouse360.Application.IdentityManagement.Commands;
using Warehouse360.Core.IdentityManagement.Repositories;

namespace Warehouse360.Application.IdentityManagement.Handlers;

public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public AssignRoleToUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        var role = await _roleRepository.GetByIdAsync(request.RoleId);

        user.AssignRole(role);
        await _userRepository.UpdateAsync(user);
    }
}