using FluentValidation;
using Warehouse360.Application.IdentityManagement.Commands;

namespace Warehouse360.Application.IdentityManagement.Validators;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email format");
        RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters long");
    }
}