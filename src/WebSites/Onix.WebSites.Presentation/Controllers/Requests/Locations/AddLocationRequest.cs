using Onix.WebSites.Application.Commands.Locations.Add;

namespace Onix.WebSites.Presentation.Controllers.Requests.Locations;

public record AddLocationRequest(
    string Name,
    string Code)
{
    public AddLocationCommand ToCommand(Guid id)
        => new AddLocationCommand(
            id, Name, Code);
}