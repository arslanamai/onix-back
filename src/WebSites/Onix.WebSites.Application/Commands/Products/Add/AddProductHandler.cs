using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;
using Onix.WebSites.Domain.Categories.Entities;
using Onix.WebSites.Domain.Categories.ValueObjects;

namespace Onix.WebSites.Application.Commands.Products.Add;

public class AddProductHandler
{
    private readonly IValidator<AddProductCommand> _validator;
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddProductHandler> _logger;

    public AddProductHandler(
        IValidator<AddProductCommand> validator,
        IWebSiteRepository webSiteRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddProductHandler> logger)
    {
        _validator = validator;
        _webSiteRepository = webSiteRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddProductCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command,cancellationToken);
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

        var productId = ProductId.NewId();
        var name = Name.Create(command.Name).Value;
        var description = Description.Create(command.Description).Value;
        var price = Price.Create(command.Price).Value;
        var link = Link.Create(command.Link).Value;
        
        var product = Product.Create(
            productId,
            name,
            description,
            price,
            link).Value;

        categoryResult.AddProduct(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Guid.NewGuid();
    }
}