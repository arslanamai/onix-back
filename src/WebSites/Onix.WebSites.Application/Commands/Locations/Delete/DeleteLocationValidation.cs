using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.Locations.Delete;

public class DeleteLocationValidation : AbstractValidator<DeleteLocationCommand>
{
    public DeleteLocationValidation()
    {
        RuleFor(a => a.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domain.Required(ConstType.WebSiteId));

        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.WebSiteId));
        
        RuleFor(a => a.LocationId)
            .NotEmpty()
            .WithError(Errors.Domain.Required(ConstType.LocationId));

        RuleFor(a => a.LocationId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.LocationId));
    }
}