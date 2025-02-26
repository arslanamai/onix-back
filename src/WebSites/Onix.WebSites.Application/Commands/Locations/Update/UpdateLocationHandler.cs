using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;

namespace Onix.WebSites.Application.Commands.Locations.Update;

public class UpdateLocationHandler
{
    private readonly IValidator<UpdateLocationCommand> _validator;
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IWebSiteUnitOfWork _webSiteUnitOfWork;
    private readonly ILogger<UpdateLocationHandler> _logger;

    public UpdateLocationHandler(
        IValidator<UpdateLocationCommand> validator,
        IWebSiteRepository webSiteRepository,
        IWebSiteUnitOfWork webSiteUnitOfWork,
        ILogger<UpdateLocationHandler> logger)
    {
        _validator = validator;
        _webSiteRepository = webSiteRepository;
        _webSiteUnitOfWork = webSiteUnitOfWork;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        UpdateLocationCommand command ,CancellationToken cancellationToken)
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

        var name = Name.Create(command.Name).Value;
        var code = Code.Create(command.Code).Value;
        
        var result = locationResult.Update(name, code);
        if (result.IsFailure)
            return result.Error.ToErrorList();
        
        await _webSiteUnitOfWork.SaveChangesAsync(cancellationToken);
        return UnitResult.Success<ErrorList>();
    }
}