using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.WebSites.AddFaq;

public class AddFaqValidator : AbstractValidator<AddFaqCommand>
{
    public AddFaqValidator()
    {
        RuleFor(c => c.WebSiteId)
            .Empty()
            .WithError(Errors.Domain.Empty(ConstType.WebSiteId));

        RuleFor(c => c.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.WebSiteId));
    }
}