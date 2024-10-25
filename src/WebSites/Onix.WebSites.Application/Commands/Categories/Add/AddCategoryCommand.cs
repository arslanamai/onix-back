using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Commands.Categories.Add;

public record AddCategoryCommand(
    Guid WebSiteId,
    Guid ParentCategoryId,
    string Name) : ICommand;