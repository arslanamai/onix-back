using Onix.WebSites.Application.Commands.Products.Add;

namespace Onix.WebSites.Presentation.Controllers.Requests;

public record AddProductRequest(
    string Name,
    string Description)
{
    public AddProductCommand ToCommand(Guid webSiteId, Guid blockId) =>
        new(webSiteId, blockId, Name, Description);
}
