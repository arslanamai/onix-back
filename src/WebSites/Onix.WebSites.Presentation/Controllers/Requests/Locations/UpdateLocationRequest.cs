using Onix.WebSites.Application.Commands.Locations.Update;

namespace Onix.WebSites.Presentation.Controllers.Requests.Locations;

public record UpdateLocationRequest(
    string Name,
    string Phone,
    string City,
    string Street,
    string Build,
    string Index)
{
    public UpdateLocationCommand ToCommand(Guid id, Guid locationId)
        => new UpdateLocationCommand(
            id, locationId,Name, Phone, City, Street, Build, Index);
}