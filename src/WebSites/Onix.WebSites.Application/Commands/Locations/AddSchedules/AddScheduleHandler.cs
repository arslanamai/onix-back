using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;
using Onix.WebSites.Domain.Locations.ValueObjects;
using Onix.WebSites.Domain.WebSites.ValueObjects;

namespace Onix.WebSites.Application.Commands.Locations.AddSchedules;

public class AddScheduleHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddScheduleHandler> _logger;
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IValidator<AddScheduleCommand> _validator;

    public AddScheduleHandler(
        IUnitOfWork unitOfWork,
        ILogger<AddScheduleHandler> logger,
        IWebSiteRepository webSiteRepository,
        IValidator<AddScheduleCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _webSiteRepository = webSiteRepository;
        _validator = validator;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        AddScheduleCommand command, CancellationToken cancellationToken = default)
    {
        var validator = await _validator.ValidateAsync(command, cancellationToken);
        if (!validator.IsValid)
            return validator.ToList();

        var webSiteId = WebSiteId.Create(command.WebSiteId);

        var webSiteResult = await _webSiteRepository
            .GetById(webSiteId, cancellationToken);
        if (webSiteResult.IsFailure)
            return webSiteResult.Error.ToErrorList();

        var locationId = LocationId.Create(command.LocationId);

        var locationResult = webSiteResult.Value.Locations
            .FirstOrDefault(l => l.Id == locationId);
        if (locationResult is null)
            return Errors.General.NotFound(locationId.Value).ToErrorList();
        
        var schedules = Schedule.Create(command.Schedules).Value;

        var result = locationResult.AddSchedule(schedules);
        if (result.IsFailure)
            return result.Error.ToErrorList();
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return webSiteResult.Value.Id.Value;
    }
}