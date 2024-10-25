using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.Locations.Update;

public class UpdateLocationValidation : AbstractValidator<UpdateLocationCommand>
{
    public UpdateLocationValidation()
    {
        RuleFor(a => a.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domain.ValueIsRequired(ConstType.WebSiteId));

        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.ValueIsInvalid(ConstType.WebSiteId));
        
        RuleFor(a => a.LocationId)
            .NotEmpty()
            .WithError(Errors.Domain.ValueIsRequired(ConstType.CategoryId));

        RuleFor(a => a.LocationId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.ValueIsInvalid(ConstType.CategoryId));
        
        RuleFor(l => l.Name)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.Name));
        
        RuleFor(l => l.Name)
            .MaximumLength(Constants.NAME_MAX_LENGHT)
            .WithError(Errors.Domain.MaxLength(ConstType.Name));
        
        RuleFor(l => l.Phone)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.Phone));
        
        RuleFor(l => l.Phone)
            .MaximumLength(Constants.PHONE_MAX_LENGHT)
            .WithError(Errors.Domain.MaxLength(ConstType.Phone));
        
        RuleFor(l => l.City)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.City));
        
        RuleFor(l => l.City)
            .MaximumLength(Constants.ADDRESS_MAX_LENGHT)
            .WithError(Errors.Domain.MaxLength(ConstType.City));
        
        RuleFor(l => l.Street)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.Street));
        
        RuleFor(l => l.Street)
            .MaximumLength(Constants.ADDRESS_MAX_LENGHT)
            .WithError(Errors.Domain.MaxLength(ConstType.Street));
        
        RuleFor(l => l.Build)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.Build));
        
        RuleFor(l => l.Build)
            .MaximumLength(Constants.ADDRESS_MAX_LENGHT)
            .WithError(Errors.Domain.MaxLength(ConstType.Build));

        RuleFor(l => l.Index)
            .MaximumLength(Constants.INDEX_MAX_LENGHT)
            .WithError(Errors.Domain.MaxLength(ConstType.Index));
    }
}