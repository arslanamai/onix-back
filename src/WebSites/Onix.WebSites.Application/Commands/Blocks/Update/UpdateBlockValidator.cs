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
            .WithError(Errors.Domains.Required(ConstType.WebSiteId));

        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.WebSiteId));

        RuleFor(a => a.BlockId)
            .NotEmpty()
            .WithError(Errors.Domains.Required(ConstType.BlockId));

        RuleFor(a => a.BlockId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.BlockId));

        RuleFor(a => a.Code)
            .MaximumLength(Constants.CODE_MAX_LENGTH)
            .WithError(Errors.Domains.MaxLength(ConstType.Code));
    }
}