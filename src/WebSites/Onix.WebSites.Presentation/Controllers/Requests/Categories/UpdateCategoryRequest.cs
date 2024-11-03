using Onix.WebSites.Application.Commands.Categories.Update;

namespace Onix.WebSites.Presentation.Controllers.Requests.Categories;

public record UpdateCategoryRequest(
    string Name)
{
    public UpdateCategoryCommand ToCommand(Guid id, Guid categoryId) 
        => new UpdateCategoryCommand(id, categoryId, Name);
}