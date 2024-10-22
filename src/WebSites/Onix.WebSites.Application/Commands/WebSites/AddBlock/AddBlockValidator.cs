using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Domain.Blocks.ValueObjects;

namespace Onix.WebSites.Application.Commands.WebSites.AddBlock;

public class AddBlockValidator : AbstractValidator<AddBlockCommand>
{
    public AddBlockValidator()
    {
        RuleFor(a => a.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domain.ValueIsRequired(ConstType.WebSiteId));

        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.ValueIsInvalid(ConstType.WebSiteId));
        
        RuleFor(a => a.Code)
            .MaximumLength(Constants.CODE_MAX_LENGHT)
            .WithError(Errors.Domain.MaxLength(ConstType.Code));
    }
}