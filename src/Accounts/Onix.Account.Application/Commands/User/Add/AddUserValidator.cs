using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.Account.Application.Commands.User.Add;

public class AddUserValidator : AbstractValidator<AddUserCommand>
{
    public AddUserValidator()
    {
        RuleFor(u => u.Email.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.Email));
        
        RuleFor(u => u.Email)
            .NotEmpty()
            .WithError(Errors.Domains.Required(ConstType.Email));
        
        RuleFor(u => u.Email)
            .MaximumLength(Constants.EMAIL_MAX_LENGTH)
            .WithError(Errors.Domains.MaxLength(ConstType.Email));
    }
}