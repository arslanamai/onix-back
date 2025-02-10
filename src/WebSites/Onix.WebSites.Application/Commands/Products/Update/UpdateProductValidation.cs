using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.Products.Update;

public class UpdateProductValidation : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidation()
    {
        RuleFor(a => a.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domains.Empty(ConstType.WebSiteId));
        
        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.WebSiteId));
        
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