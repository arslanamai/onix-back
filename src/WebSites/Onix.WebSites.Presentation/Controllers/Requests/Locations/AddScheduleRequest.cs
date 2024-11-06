using Onix.WebSites.Application.Commands.Locations.AddSchedules;
using Onix.WebSites.Domain.Locations.ValueObjects;

namespace Onix.WebSites.Presentation.Controllers.Requests.Locations;

public record AddScheduleRequest(
    List<Schedule> Schedules)
{
    public AddScheduleCommand ToCommand(Guid id, Guid locationId)
        => new AddScheduleCommand(id, locationId, Schedules);
}