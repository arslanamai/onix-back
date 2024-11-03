using Onix.Core.Abstraction;
using Onix.WebSites.Domain.Locations.ValueObjects;

namespace Onix.WebSites.Application.Commands.Locations.AddSchedules;

public record AddScheduleCommand(
    Guid WebSiteId,
    Guid LocationId,
    List<Schedule> Schedules) : ICommand;