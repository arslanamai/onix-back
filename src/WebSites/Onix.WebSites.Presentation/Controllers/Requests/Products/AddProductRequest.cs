using Onix.WebSites.Application.Commands.Products.Add;

namespace Onix.WebSites.Presentation.Controllers.Requests.Products;

public record AddProductRequest(
    string Name,
    string Description,
    string Price,
    string Link)
{
    public AddProductCommand ToCommand(Guid webSiteId, Guid categoryId) =>
        new(webSiteId, categoryId, Name, Description, Price, Link);
}
