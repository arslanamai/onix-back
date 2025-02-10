using Onix.WebSites.Application.Commands.Locations.Update;

namespace Onix.WebSites.Presentation.Controllers.Requests.Locations;

public record UpdateLocationRequest(
    string Name,
    string Code)
{
    public UpdateLocationCommand ToCommand(Guid id, Guid locationId)
        => new UpdateLocationCommand(
            id, locationId, Name, Code);
}