using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;

namespace Onix.WebSites.Application.Commands.Products.Add;

public class AddProductValidation : AbstractValidator<AddProductCommand>
{
    public AddProductValidation()
    {
        RuleFor(a => a.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.WebSiteId));
        
        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.WebSiteId));
        
        RuleFor(a => a.CategoryId)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.CategoryId));
        
        RuleFor(a => a.CategoryId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.CategoryId));
        
        RuleFor(a => a.Name)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.Name));
        
        RuleFor(a => a.Name)
            .MaximumLength(Constants.NAME_MAX_LENGTH)
            .WithError(Errors.Domain.MaxLength(ConstType.Name));

        RuleFor(a => a.Description)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.Description));
        
        RuleFor(a => a.Description)
            .MaximumLength(Constants.DESCRIPTION_MAX_LENGTH)
            .WithError(Errors.Domain.MaxLength(ConstType.Description));
        
        RuleFor(a => a.Price)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.Price));
        
        RuleFor(a => a.Price)
            .MaximumLength(Constants.PRICE_MAX_LENGTH)
            .WithError(Errors.Domain.MaxLength(ConstType.Description));
        
        RuleFor(a => a.Link)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.Link));
        
        RuleFor(a => a.Link)
            .MaximumLength(Constants.LINK_MAX_LENGTH)
            .WithError(Errors.Domain.MaxLength(ConstType.Link));
    }
}