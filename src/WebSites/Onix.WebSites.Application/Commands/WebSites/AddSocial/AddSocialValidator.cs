using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.WebSites.AddSocial;

public class AddSocialValidator : AbstractValidator<AddSocialCommand>
{
    public AddSocialValidator()
    {
        RuleFor(c => c.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.WebSiteId));

        RuleFor(c => c.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.WebSiteId));
    }
}