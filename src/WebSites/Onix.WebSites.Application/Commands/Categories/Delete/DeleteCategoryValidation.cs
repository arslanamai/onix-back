using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.Categories.Delete;

public class DeleteCategoryValidation : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryValidation()
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
    }
}