using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.Categories.Update;

public class UpdateCategoryValidation : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidation()
    {
        RuleFor(a => a.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domain.Required(ConstType.WebSiteId));

        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.WebSiteId));
        
        RuleFor(a => a.CategoryId)
            .NotEmpty()
            .WithError(Errors.Domain.Required(ConstType.CategoryId));

        RuleFor(a => a.CategoryId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.CategoryId));
        
        RuleFor(a => a.Name)
            .NotEmpty()
            .WithError(Errors.Domain.Required(ConstType.Name));
        
        RuleFor(a => a.Name)
            .MaximumLength(Constants.NAME_MAX_LENGTH)
            .WithError(Errors.Domain.MaxLength(ConstType.Name));
    }
}