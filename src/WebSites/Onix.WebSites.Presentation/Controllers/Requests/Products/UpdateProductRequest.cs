using Onix.WebSites.Application.Commands.Products.Update;

namespace Onix.WebSites.Presentation.Controllers.Requests.Products;

public record UpdateProductRequest(
    string Name,
    string Description,
    string Price,
    string Link)
{
    public UpdateProductCommand ToCommand(
        Guid webSiteId, Guid categoryId, Guid productId)
        => new(webSiteId, categoryId, productId,
            Name, Description, Price, Link);
}