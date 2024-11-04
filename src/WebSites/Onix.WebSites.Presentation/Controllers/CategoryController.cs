using Microsoft.AspNetCore.Mvc;
using Onix.Framework;
using Onix.WebSites.Application.Commands.Categories.Add;
using Onix.WebSites.Application.Commands.Categories.Delete;
using Onix.WebSites.Application.Commands.Categories.Update;
using Onix.WebSites.Presentation.Controllers.Requests.Categories;

namespace Onix.WebSites.Presentation.Controllers;

public class CategoryController : ApplicationController
{
    [HttpPost("/website/{id:guid}/category")]
    public async Task<IActionResult> AddCategory(
        [FromBody] AddCategoryRequest request,
        [FromServices] AddCategoryHandler handler,
        [FromRoute] Guid id,
        [FromRoute] Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(
            request.ToCommand(id, categoryId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("/website/{id:guid}/category/{categoryId:guid}")]
    public async Task<IActionResult> UpdateCategory(
        [FromRoute] Guid id,
        [FromRoute] Guid categoryId,
        [FromServices] UpdateCategoryHandler handler,
        [FromBody] UpdateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(
            request.ToCommand(id, categoryId), cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.ToString());
    }
    
    [HttpDelete("/website/{id:guid}/category/{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategory(
        [FromRoute] Guid id,
        [FromRoute] Guid categoryId,
        [FromServices] DeleteCategoryHandler handler,
        [FromBody] DeleteCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(
            request.ToCommand(id, categoryId), cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.ToString());
    }
}