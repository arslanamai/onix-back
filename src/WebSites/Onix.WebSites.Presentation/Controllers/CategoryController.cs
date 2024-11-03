using Microsoft.AspNetCore.Mvc;
using Onix.Framework;
using Onix.WebSites.Application.Commands.Categories.Add;
using Onix.WebSites.Presentation.Controllers.Requests;
using Onix.WebSites.Presentation.Controllers.Requests.Categories;

namespace Onix.WebSites.Presentation.Controllers;

public class CategoryController : ApplicationController
{
    [HttpPost("/website/{id:guid}/category")]
    public async Task<IActionResult> AddCategory(
        [FromBody] AddCategoryRequest request,
        [FromServices] AddCategoryHandle handle,
        [FromRoute] Guid id,
        [FromRoute] Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        var result = await handle.Handle(
            request.ToCommand(id, categoryId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}