using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;
using Onix.WebSites.Domain.Products;

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

        var productId = ProductId.NewId();
        var name = Name.Create(command.Name).Value;
        var code = Code.Create(command.Code).Value;
        var product = Product.Create(
            productId,
            name,
            code).Value;

        webSiteResult.Value.AddProduct(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return product.Id.Value;
    }
}