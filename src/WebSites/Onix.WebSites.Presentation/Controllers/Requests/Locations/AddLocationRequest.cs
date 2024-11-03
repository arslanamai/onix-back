using Onix.WebSites.Application.Commands.Locations.Add;

namespace Onix.WebSites.Presentation.Controllers.Requests.Locations;

public record AddLocationRequest(
    string Name,
    string Phone,
    string City,
    string Street,
    string Build,
    string Index)
{
    public AddLocationCommand ToCommand(Guid id)
        => new AddLocationCommand(
            id, Name, Phone, City, Street, Build, Index);
}