using MediatR;
using Warehouse360.Application.IdentityManagement.Commands;
using Warehouse360.Application.IdentityManagement.Dtos;
using Warehouse360.Core.IdentityManagement.Entities;
using Warehouse360.Core.IdentityManagement.Repositories;

namespace Warehouse360.Application.IdentityManagement.Handlers;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Role(request.Name);
        await _roleRepository.AddAsync(role);

        return new RoleDto(role.Id, role.Name);
    }
}