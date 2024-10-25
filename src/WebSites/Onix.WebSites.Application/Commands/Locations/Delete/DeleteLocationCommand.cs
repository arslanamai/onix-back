using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Commands.Locations.Delete;

public record DeleteLocationCommand(
    Guid WebSiteId,
    Guid LocationId) : ICommand;