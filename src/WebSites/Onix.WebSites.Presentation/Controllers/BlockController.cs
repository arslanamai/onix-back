using Microsoft.AspNetCore.Mvc;
using Onix.Framework;
using Onix.WebSites.Application.Commands.Blocks.Add;
using Onix.WebSites.Application.Commands.Blocks.Delete;
using Onix.WebSites.Application.Commands.Blocks.Update;
using Onix.WebSites.Application.Queries.WebSites.GetByIdWithBlocks;
using Onix.WebSites.Presentation.Controllers.Requests.Blocks;

namespace Onix.WebSites.Presentation.Controllers;

public class BlockController : ApplicationController
{
    /*[HttpGet("website/{id:guid}/block")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid id,
        [FromServices] GetBlocksHandler handler,
        CancellationToken cancellationToken)
    {
        var query = new GetBlockRequest();
        var result = await handler.Handle(query.ToQuery(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }*/
    
    [HttpPost("/website/{id:guid}/block")]
    public async Task<IActionResult> Add(
        [FromRoute] Guid id,
        [FromServices] AddBlockHandler handler,
        [FromBody] AddBlockRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(
            request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpPut("/website/{id:guid}/block/{blockId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromRoute] Guid blockId,
        [FromServices] UpdateBlockHandler handler,
        [FromBody] UpdateBlockRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(
            request.ToCommand(id, blockId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.ToString());
    }
    
    [HttpDelete("/website/{id:guid}/block/{blockId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromRoute] Guid blockId,
        [FromServices] DeleteBlockHandle handler,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteBlockRequest();
        var result = await handler.Handle(
            request.ToCommand(id, blockId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.ToString());
    }
}