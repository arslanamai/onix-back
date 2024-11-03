using Onix.WebSites.Application.Commands.Categories.Add;

namespace Onix.WebSites.Presentation.Controllers.Requests.Categories;

public record AddCategoryRequest(
    string Name)
{
    public AddCategoryCommand ToCommand(Guid id, Guid parentId) =>
        new AddCategoryCommand(id, parentId,Name);
};