using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.WebSites.Update;

public class UpdateWebSiteValidator : AbstractValidator<UpdateWebSiteCommand>
{
    public UpdateWebSiteValidator()
    {
        RuleFor(a => a.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domains.Required(ConstType.WebSiteId));

        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.WebSiteId));
        
        RuleFor(c => c.Url)
            .NotEmpty()
            .WithError(Errors.Domains.Required(ConstType.SubDamain));
        
        RuleFor(c => c.Url)
            .MaximumLength(Constants.SUBDOMAIN_MAX_LENGTH)
            .WithError(Errors.Domains.MaxLength(ConstType.SubDamain));
        
        RuleFor(c => c.Url)
            .MinimumLength(Constants.URL_MIN_LENGTH)
            .WithError(Errors.Domains.MinLength(ConstType.SubDamain));

        RuleFor(c => c.Url)
            .Matches(Constants.URL_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.SubDamain));

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithError(Errors.Domains.Required(ConstType.Name));

        RuleFor(c => c.Name)
            .MaximumLength(Constants.NAME_MAX_LENGTH)
            .WithError(Errors.Domains.MaxLength(ConstType.Name));
        
        RuleFor(c => c.Name)
            .MinimumLength(Constants.NAME_MIN_LENGTH)
            .WithError(Errors.Domains.MaxLength(ConstType.Name));
    }
}