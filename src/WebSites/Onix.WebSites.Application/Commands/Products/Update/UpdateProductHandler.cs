using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;

namespace Onix.WebSites.Application.Commands.Products.Update;

public class UpdateProductHandler
{
    private readonly ILogger<UpdateProductHandler> _logger;
    private readonly IWebSiteUnitOfWork _webSiteUnitOfWork;
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IValidator<UpdateProductCommand> _validator;

    public UpdateProductHandler(
        ILogger<UpdateProductHandler> logger,
        IWebSiteUnitOfWork webSiteUnitOfWork,
        IWebSiteRepository webSiteRepository,
        IValidator<UpdateProductCommand> validator)
    {
        _logger = logger;
        _webSiteUnitOfWork = webSiteUnitOfWork;
        _webSiteRepository = webSiteRepository;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(
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
        
        var productId = ProductId.Create(command.ProductId);

        var productResult = webSiteResult.Value.Products
            .FirstOrDefault(p => p.Id == productId);
        if (productResult is null)
            return Errors.General.NotFound(ConstType.Product).ToErrorList();
            
        var name = Name.Create(command.Name).Value;
        var code = Code.Create(command.Code).Value;

        var result = productResult.Update(
            name,
            code);

        if (result.IsFailure)
            return result.Error.ToErrorList();

        await _webSiteUnitOfWork.SaveChangesAsync(cancellationToken);
        return UnitResult.Success<ErrorList>();
    }
}