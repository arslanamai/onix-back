using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.Account.Application.Commands.User.Delete;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .WithError(Errors.Domains.Required(ConstType.UserId));

        RuleFor(u => u.Id.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.UserId));
    }
}