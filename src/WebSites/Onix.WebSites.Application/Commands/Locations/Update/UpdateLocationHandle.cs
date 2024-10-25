using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;
using Onix.WebSites.Domain.Locations.ValueObjects;

namespace Onix.WebSites.Application.Commands.Locations.Update;

public class UpdateLocationHandle
{
    private readonly IValidator<UpdateLocationCommand> _validator;
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateLocationHandle> _logger;

    public UpdateLocationHandle(
        IValidator<UpdateLocationCommand> validator,
        IWebSiteRepository webSiteRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateLocationHandle> logger)
    {
        _validator = validator;
        _webSiteRepository = webSiteRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateLocationCommand command ,CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command,cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var webSiteId = WebSiteId.Create(command.WebSiteId);
        
        var webSiteResult = await _webSiteRepository
            .GetByIdWithCategories(webSiteId, cancellationToken);
        if (webSiteResult.IsFailure)
            return webSiteResult.Error.ToErrorList();

        var locationId = LocationId.Create(command.LocationId);
        
        var locationResult = webSiteResult.Value.Locations
            .FirstOrDefault(b => b.Id == locationId);
        if (locationResult is null)
            return Errors.General.NotFound(locationId.Value).ToErrorList();

        var name = Name.Create(command.Name).Value;
        var phone = Phone.Create(command.Phone).Value;
        var address = Address.Create(
            command.City,
            command.Street,
            command.Build,
            command.Index).Value;
        
        var result = locationResult.Update(name, phone, address);
        if (result.IsFailure)
            return result.Error.ToErrorList();
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return locationResult.Id.Value;
    }
}