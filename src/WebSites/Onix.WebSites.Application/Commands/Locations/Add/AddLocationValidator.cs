using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.Locations.Add;

public class AddLocationValidator : AbstractValidator<AddLocationCommand>
{
    public AddLocationValidator()
    {
        RuleFor(l => l.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domains.Empty(ConstType.WebSiteId));
        
        RuleFor(c => c.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.WebSiteId));

        RuleFor(l => l.Name)
            .NotEmpty()
            .WithError(Errors.Domains.Empty(ConstType.Name));
        
        RuleFor(l => l.Name)
            .MaximumLength(Constants.NAME_MAX_LENGTH)
            .WithError(Errors.Domains.MaxLength(ConstType.Name));
        
        RuleFor(l => l.Code)
            .NotEmpty()
            .WithError(Errors.Domains.Empty(ConstType.Code));
        
        RuleFor(l => l.Code)
            .MaximumLength(Constants.CODE_MAX_LENGTH)
            .WithError(Errors.Domains.MaxLength(ConstType.Code));
    }
}