using Onix.WebSites.Application.Commands.Categories.Delete;

namespace Onix.WebSites.Presentation.Controllers.Requests.Categories;

public record DeleteCategoryRequest
{
    public DeleteCategoryCommand ToCommand(Guid id, Guid categoryId)
        => new DeleteCategoryCommand(id, categoryId);
}