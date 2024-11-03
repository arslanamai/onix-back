using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.Blocks.Add;

public class AddBlockValidator : AbstractValidator<AddBlockCommand>
{
    public AddBlockValidator()
    {
        RuleFor(a => a.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domain.Required(ConstType.WebSiteId));

        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.WebSiteId));
        
        RuleFor(a => a.Code)
            .MaximumLength(Constants.CODE_MAX_LENGTH)
            .WithError(Errors.Domain.MaxLength(ConstType.Code));
    }
}