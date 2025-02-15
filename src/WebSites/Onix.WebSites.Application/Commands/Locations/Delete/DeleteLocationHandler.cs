using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;

namespace Onix.WebSites.Application.Commands.Locations.Delete;

public class DeleteLocationHandler
{
    private readonly IValidator<DeleteLocationCommand> _validator;
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IWebSiteUnitOfWork _webSiteUnitOfWork;
    private readonly ILogger<DeleteLocationHandler> _logger;

    public DeleteLocationHandler(
        IValidator<DeleteLocationCommand> validator,
        IWebSiteRepository webSiteRepository,
        IWebSiteUnitOfWork webSiteUnitOfWork,
        ILogger<DeleteLocationHandler> logger)
    {
        _validator = validator;
        _webSiteRepository = webSiteRepository;
        _webSiteUnitOfWork = webSiteUnitOfWork;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        DeleteLocationCommand command ,CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command,cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var webSiteId = WebSiteId.Create(command.WebSiteId);
        
        var webSiteResult = await _webSiteRepository
            .GetByIdWithLocation(webSiteId, cancellationToken);
        if (webSiteResult.IsFailure)
            return webSiteResult.Error.ToErrorList();

        var locationId = LocationId.Create(command.LocationId);
        
        var locationResult = webSiteResult.Value.Locations
            .FirstOrDefault(b => b.Id == locationId);
        if (locationResult is null)
            return Errors.General.NotFound(ConstType.Location).ToErrorList();

        var result = webSiteResult.Value.RemoveLocation(locationResult);
        if (result.IsFailure)
            return result.Error.ToErrorList();
        
        await _webSiteUnitOfWork.SaveChangesAsync(cancellationToken);
        return UnitResult.Success<ErrorList>();
    }
}