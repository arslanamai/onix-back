using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;
using Onix.WebSites.Domain.Categories.ValueObjects;

namespace Onix.WebSites.Application.Commands.Products.Update;

public class UpdateProductHandler
{
    private readonly ILogger<UpdateProductHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IValidator<UpdateProductCommand> _validator;

    public UpdateProductHandler(
        ILogger<UpdateProductHandler> logger,
        IUnitOfWork unitOfWork,
        IWebSiteRepository webSiteRepository,
        IValidator<UpdateProductCommand> validator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _webSiteRepository = webSiteRepository;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var webSiteId = WebSiteId.Create(command.WebSiteId);

        var webSiteResult = await _webSiteRepository
            .GetById(webSiteId, cancellationToken);

        if (webSiteResult.IsFailure)
            return webSiteResult.Error.ToErrorList();

        var categoryId = CategoryId.Create(command.CategoryId);
        
        var categoryResult = webSiteResult.Value.Categories
            .FirstOrDefault(c => c.Id == categoryId);
        if (categoryResult is null)
            return Errors.General.NotFound(categoryId.Value).ToErrorList();

        var productId = ProductId.Create(command.ProductId);

        var productResult = categoryResult.Products
            .FirstOrDefault(p => p.Id == productId);
        if (productResult is null)
            return Errors.General.NotFound(productId.Value).ToErrorList();
            
        var name = Name.Create(command.Name).Value;
        var description = Description.Create(command.Description).Value;
        var price = Price.Create(command.Price).Value;
        var link = Link.Create(command.Link).Value;

        var result = productResult.Update(
            name,
            description,
            price,
            link);

        if (result.IsFailure)
            return result.Error.ToErrorList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return productResult.Id.Value;
    }
}