using Microsoft.AspNetCore.Mvc;
using Onix.Framework;
using Onix.WebSites.Application.Commands.Products.Add;
using Onix.WebSites.Application.Commands.Products.Delete;
using Onix.WebSites.Application.Commands.Products.Update;
using Onix.WebSites.Presentation.Controllers.Requests.Products;

namespace Onix.WebSites.Presentation.Controllers;

public class ProductController : ApplicationController
{
    [HttpPost("website/{id:guid}/category/{categoryId:guid}/product")]
    public async Task<IActionResult> Add(
        [FromRoute] Guid id,
        [FromRoute] Guid categoryId,
        [FromServices] AddProductHandler handler,
        [FromBody] AddProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(
            request.ToCommand(id, categoryId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpPut("website/{id:guid}/category/{categoryId:guid}/product/{productId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid productId,
        [FromServices] UpdateProductHandler handler,
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(
            request.ToCommand(id, categoryId, productId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.ToString());
    }
    
    [HttpDelete("website/{id:guid}/category/{categoryId:guid}/product/{productId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid productId,
        [FromServices] DeleteProductHandler handler,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteProductRequest();
        var result = await handler.Handle(
            request.ToCommand(id, categoryId, productId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.ToString());
    }
}