using Onix.WebSites.Application.Commands.Products.Delete;

namespace Onix.WebSites.Presentation.Controllers.Requests.Products;

public record DeleteProductRequest
{
    public DeleteProductCommand ToCommand(
        Guid webSiteId, Guid productId)
        => new DeleteProductCommand(webSiteId, productId);
}