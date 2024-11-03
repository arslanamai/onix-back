using Onix.WebSites.Application.Commands.Locations.Delete;

namespace Onix.WebSites.Presentation.Controllers.Requests.Locations;

public record DeleteLocationRequest
{
    public DeleteLocationCommand ToCommand(
        Guid id, Guid locationId)
            => new DeleteLocationCommand(id, locationId);
}