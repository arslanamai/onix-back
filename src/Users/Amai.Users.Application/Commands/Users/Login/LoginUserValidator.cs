using Amai.Core.Validation;
using Amai.SharedKernel;
using Amai.Users.Application.Commands.Users.Add;
using FluentValidation;

namespace Amai.Users.Application.Commands.Users.Login;

public class LoginUserValidator : AbstractValidator<AddUserCommand>
{
    public LoginUserValidator()
    {
        RuleFor(u => u.Email.ToString())
            .Matches(Constants.EMAIL_REGEX)
            .WithError(Errors.Validation.Invalid(ConstType.Email));
        
        RuleFor(u => u.Email)
            .NotEmpty()
            .WithError(Errors.Validation.Required(ConstType.Email));
        
        RuleFor(u => u.Email)
            .MaximumLength(Constants.EMAIL_MAX_LENGTH)
            .WithError(Errors.Validation.MaxLength(ConstType.Email));
    }
}