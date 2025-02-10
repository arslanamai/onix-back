using FluentValidation;
using Onix.Core.Validation;
using Onix.SharedKernel;

namespace Onix.WebSites.Application.Commands.Products.Delete;

public class DeleteProductValidation : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidation()
    {
        RuleFor(a => a.WebSiteId)
            .NotEmpty()
            .WithError(Errors.Domains.Empty(ConstType.WebSiteId));
        
        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.WebSiteId));
        
        RuleFor(a => a.ProductId)
            .NotEmpty()
            .WithError(Errors.Domains.Empty(ConstType.ProductId));
        
        RuleFor(a => a.ProductId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domains.Invalid(ConstType.ProductId));
    }
}