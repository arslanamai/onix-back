using Amai.Core.Validation;
using Amai.SharedKernel;
using FluentValidation;

namespace Amai.Users.Application.Commands.Users.Add;

public class AddUserValidator : AbstractValidator<AddUserCommand>
{
    public AddUserValidator()
    {
        RuleFor(u => u.Email.ToString())
            .Matches(Constants.EMAIL_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.Email));
        
        RuleFor(u => u.Email)
            .NotEmpty()
            .WithError(Errors.Domains.Required(ConstType.Email));
        
        RuleFor(u => u.Email)
            .MaximumLength(Constants.EMAIL_MAX_LENGTH)
            .WithError(Errors.Domains.MaxLength(ConstType.Email));
    }
}