namespace Onix.WebSites.Application.Commands.Products.Delete;

public record DeleteProductCommand(
    Guid WebSiteId,
    Guid CategoryId,
    Guid ProductId);