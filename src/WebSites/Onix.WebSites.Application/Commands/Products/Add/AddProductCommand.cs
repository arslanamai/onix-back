using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Commands.Products.Add;

public record AddProductCommand(
    Guid WebSiteId,
    string Name,
    string Code) : ICommand;