using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Commands.Products.Update;

public record UpdateProductCommand(
    Guid WebSiteId,
    Guid ProductId,
    string Name,
    string Code) : ICommand;