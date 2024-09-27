using FluentValidation;
using MediatR;
using Warehouse360.Application.IdentityManagement.Commands;
using Warehouse360.Application.IdentityManagement.Dtos;
using Warehouse360.Core.IdentityManagement.Services;
using Warehouse360.Core.IdentityManagement.ValueObjects;

namespace Warehouse360.Application.IdentityManagement.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IValidator<CreateUserCommand> _validator;
    private readonly IUserService _userService;

    public CreateUserCommandHandler(
        IValidator<CreateUserCommand> validator, 
        IUserService userService)
    {
        _validator = validator;
        _userService = userService;
    }
    
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var user = await _userService.CreateUser(
            request.Username.ToLower(), 
            new Email(request.Email.ToLower()), 
            request.Password);

        return new UserDto(user.Id, user.Username, user.Email.Value);
    }
}