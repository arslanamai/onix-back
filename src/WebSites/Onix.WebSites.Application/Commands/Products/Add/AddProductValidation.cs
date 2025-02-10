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
            .WithError(Errors.Domains.Empty(ConstType.WebSiteId));
        
        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.WebSiteId));
        
        RuleFor(a => a.CategoryId)
            .NotEmpty()
            .WithError(Errors.Domains.Empty(ConstType.CategoryId));
        
        RuleFor(a => a.CategoryId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.CategoryId));
        
        RuleFor(a => a.Name)
            .NotEmpty()
            .WithError(Errors.Domains.Empty(ConstType.Name));
        
        RuleFor(a => a.Name)
            .MaximumLength(Constants.NAME_MAX_LENGTH)
            .WithError(Errors.Domains.MaxLength(ConstType.Name));

        RuleFor(a => a.Code)
            .NotEmpty()
            .WithError(Errors.Domains.Empty(ConstType.Code));
        
        RuleFor(a => a.Code)
            .MaximumLength(Constants.CODE_MAX_LENGTH)
            .WithError(Errors.Domains.MaxLength(ConstType.Code));
    }
}