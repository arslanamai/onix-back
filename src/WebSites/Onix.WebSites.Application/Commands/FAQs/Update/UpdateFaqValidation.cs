using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.FAQs.Update;

public class UpdateFaqValidation : AbstractValidator<UpdateFaqCommand>
{
    public UpdateFaqValidation()
    {
        RuleFor(a => a.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domain.Required(ConstType.WebSiteId));

        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.WebSiteId));
        
        RuleFor(a => a.Question)
            .NotEmpty()
            .WithError(Errors.Domain.Required(ConstType.Question));
        
        RuleFor(a => a.Question)
            .MaximumLength(Constants.NAME_MAX_LENGTH)
            .WithError(Errors.Domain.MaxLength(ConstType.Question));
        
        RuleFor(a => a.Answer)
            .NotEmpty()
            .WithError(Errors.Domain.Required(ConstType.Answer));
        
        RuleFor(a => a.Answer)
            .MaximumLength(Constants.NAME_MAX_LENGTH)
            .WithError(Errors.Domain.MaxLength(ConstType.Answer));
    }
}