using Onix.SharedKernel.ValueObjects;
using Onix.WebSites.Application.Commands.Products.Update;

namespace Onix.WebSites.Presentation.Controllers.Requests.Products;

public record UpdateProductRequest(
    string Name,
    string Code)
{
    public UpdateProductCommand ToCommand(
        Guid webSiteId, Guid categoryId, Guid productId)
        => new(webSiteId, productId, Name, Code);
}