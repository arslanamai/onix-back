using Onix.WebSites.Application.Commands.Products.Add;

namespace Onix.WebSites.Presentation.Controllers.Requests.Products;

public record AddProductRequest(
    string Name,
    string Code)
{
    public AddProductCommand ToCommand(Guid webSiteId) =>
        new(webSiteId, Name, Code);
}
