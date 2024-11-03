using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.WebSites.UpdateContact;
 
public class UpdateContactValidator : AbstractValidator<UpdateContactCommand>
{
    public UpdateContactValidator()
    {
        RuleFor(c => c.WebSiteId)
            .Empty()
            .WithError(Errors.Domain.Empty(ConstType.WebSiteId));

        RuleFor(c => c.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.WebSiteId));

        RuleFor(c => c.Phone)
            .Empty()
            .WithError(Errors.Domain.Empty(ConstType.Phone));
        
        RuleFor(c => c.Email)
            .Empty()
            .WithError(Errors.Domain.Empty(ConstType.Email));
        
        RuleFor(c => c.Phone)
            .MaximumLength(Constants.PHONE_MAX_LENGTH)
            .WithError(Errors.Domain.MaxLength(ConstType.Phone));
        
        RuleFor(c => c.Email)
            .MaximumLength(Constants.EMAIL_MAX_LENGTH)
            .WithError(Errors.Domain.MaxLength(ConstType.Email));
    }
}