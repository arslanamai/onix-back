using Microsoft.AspNetCore.Mvc;
using Onix.Framework;
using Onix.WebSites.Application.Commands.WebSites.Create;
using Onix.WebSites.Application.Commands.WebSites.Update;
using Onix.WebSites.Application.Queries.WebSites.GetById;
using Onix.WebSites.Presentation.Controllers.Requests;

namespace Onix.WebSites.Presentation.Controllers;

public class WebSiteController : ApplicationController
{
    [HttpGet("/website/{id:guid}")]
    public async Task<IActionResult> GetWebSiteById(
        [FromRoute] Guid id,
        [FromServices] GetWebSiteByIdHandle handle,
        CancellationToken cancellationToken = default)
    {
        var query = new GetWebSiteByIdQuery(id);
        var result = await handle.Handle(query, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpPost("/website")]
    public async Task<IActionResult> Create(
        [FromServices] CreateWebSiteHandler handler,
        [FromBody] CreateWebSiteRequest request,
        CancellationToken cancellationToken = default)
    { 
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }

    [HttpPut("/website/{id:guid}")]
    public async Task<IActionResult> Update(
        [FromServices] UpdateWebSiteHandle handle,
        [FromBody] UpdateWebSiteRequest request,
        
        CancellationToken cancellationToken = default)
    {
        var result = await handle.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}