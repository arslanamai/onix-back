using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.Account.Application.Commands.User.Update;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .WithError(Errors.Domains.Required(ConstType.UserId));

        RuleFor(u => u.Id.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.UserId));
        
        RuleFor(a => a.Email.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.Email));
        
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithError(Errors.Domains.Required(ConstType.Email));
        
        RuleFor(c => c.Email)
            .MaximumLength(Constants.EMAIL_MAX_LENGTH)
            .WithError(Errors.Domains.MaxLength(ConstType.Email));
    }
}