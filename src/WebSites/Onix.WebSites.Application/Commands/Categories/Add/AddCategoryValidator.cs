using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.Categories.Add;

public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
{
    public AddCategoryValidator()
    {
        RuleFor(c => c.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.WebSiteId));

        RuleFor(c => c.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.WebSiteId));
        
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithError(Errors.Domain.Required(ConstType.Name));

        RuleFor(c => c.Name)
            .MaximumLength(Constants.NAME_MAX_LENGTH)
            .WithError(Errors.Domain.MaxLength(ConstType.Name));
    }
}