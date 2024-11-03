using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.Locations.AddSchedules;

public class AddScheduleValidator : AbstractValidator<AddScheduleCommand>
{
    public AddScheduleValidator()
    {
        RuleFor(a => a.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domain.Required(ConstType.WebSiteId));

        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.WebSiteId));
        
        RuleFor(a => a.LocationId)
            .NotEmpty()
            .WithError(Errors.Domain.Required(ConstType.CategoryId));

        RuleFor(a => a.LocationId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.CategoryId));
    }
}