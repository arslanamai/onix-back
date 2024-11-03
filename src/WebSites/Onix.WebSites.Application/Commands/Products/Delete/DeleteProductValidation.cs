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
            .WithError(Errors.Domain.Empty(ConstType.WebSiteId));
        
        RuleFor(a => a.WebSiteId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.WebSiteId));
        
        RuleFor(a => a.CategoryId)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.CategoryId));
        
        RuleFor(a => a.CategoryId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.CategoryId));
        
        RuleFor(a => a.ProductId)
            .NotEmpty()
            .WithError(Errors.Domain.Empty(ConstType.ProductId));
        
        RuleFor(a => a.ProductId.ToString())
            .Matches(Constants.ID_REGEX)
            .WithError(Errors.Domain.Invalid(ConstType.ProductId));
    }
}