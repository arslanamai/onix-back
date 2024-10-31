using Microsoft.AspNetCore.Mvc;
using Onix.Framework;
using Onix.WebSites.Application.Commands.Blocks.Add;
using Onix.WebSites.Presentation.Controllers.Requests;

namespace Onix.WebSites.Presentation.Controllers;

public class BlockController : ApplicationController
{
    [HttpPost("/website/{id:guid}/block")]
    public async Task<IActionResult> AddBlock(
        [FromRoute] Guid id,
        [FromServices] AddBlockHandler handler,
        [FromBody] AddBLockRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}