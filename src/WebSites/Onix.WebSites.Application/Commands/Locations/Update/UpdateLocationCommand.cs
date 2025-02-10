using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Commands.Locations.Update;

public record UpdateLocationCommand(
    Guid WebSiteId,
    Guid LocationId,
    string Name,
    string Code) : ICommand;