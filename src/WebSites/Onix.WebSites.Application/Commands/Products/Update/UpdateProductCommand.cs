using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Commands.Products.Update;

public record UpdateProductCommand(
    Guid WebSiteId,
    Guid CategoryId,
    Guid ProductId,
    string Name,
    string Description,
    string Price,
    string Link) : ICommand;