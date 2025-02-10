using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Commands.Products.Delete;

public record DeleteProductCommand(
    Guid WebSiteId,
    Guid ProductId) : ICommand;