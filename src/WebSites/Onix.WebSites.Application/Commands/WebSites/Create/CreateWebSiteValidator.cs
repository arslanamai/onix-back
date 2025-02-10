using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;

namespace Onix.WebSites.Application.Commands.WebSites.Create;

public class CreateWebSiteValidator : AbstractValidator<CreateWebSiteCommand>
{
    public CreateWebSiteValidator()
    {
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