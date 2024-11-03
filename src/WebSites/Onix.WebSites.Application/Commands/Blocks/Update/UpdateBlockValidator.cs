using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.Blocks.Update;

public class UpdateBlockValidator : AbstractValidator<UpdateBlockCommand>
{
    public UpdateBlockValidator()
    {
        RuleFor(a => a.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domain.Required(ConstType.WebSiteId));

        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.WebSiteId));

        RuleFor(a => a.BlockId)
            .NotEmpty()
            .WithError(Errors.Domain.Required(ConstType.BlockId));

        RuleFor(a => a.BlockId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.BlockId));

        RuleFor(a => a.Code)
            .MaximumLength(Constants.CODE_MAX_LENGTH)
            .WithError(Errors.Domain.MaxLength(ConstType.Code));
    }
}