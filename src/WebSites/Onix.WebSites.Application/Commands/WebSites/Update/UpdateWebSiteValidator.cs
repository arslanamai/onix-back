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
        
        RuleFor(c => c.SubDomain)
            .NotEmpty()
            .WithError(Errors.Domains.Required(ConstType.SubDomain));
        
        RuleFor(c => c.SubDomain)
            .MaximumLength(Constants.SUBDOMAIN_MAX_LENGTH)
            .WithError(Errors.Domains.MaxLength(ConstType.SubDomain));
        
        RuleFor(c => c.SubDomain)
            .MinimumLength(Constants.SUBDOMAIN_MIN_LENGTH)
            .WithError(Errors.Domains.MinLength(ConstType.SubDomain));

        RuleFor(c => c.SubDomain)
            .Matches(Constants.SUBDOMAIN_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.SubDomain));

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