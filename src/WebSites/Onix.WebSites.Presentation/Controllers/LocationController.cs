using Microsoft.AspNetCore.Mvc;
using Onix.Framework;
using Onix.WebSites.Application.Commands.Locations.Add;
using Onix.WebSites.Application.Commands.Locations.Delete;
using Onix.WebSites.Application.Commands.Locations.Update;
using Onix.WebSites.Presentation.Controllers.Requests.Locations;

namespace Onix.WebSites.Presentation.Controllers;

public class LocationController : ApplicationController
{
    [HttpPost("/website/{id:guid}/location")]
    public async Task<IActionResult> Add(
        [FromRoute] Guid id,
        [FromServices] AddLocationHandler handler,
        [FromBody] AddLocationRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(
            request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpPut("/website/{id:guid}/location/{locationId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromRoute] Guid locationId,
        [FromServices] UpdateLocationHandler handler,
        [FromBody] UpdateLocationRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(
            request.ToCommand(id, locationId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.ToString());
    }
    
    [HttpDelete("/website/{id:guid}/location/{locationId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromRoute] Guid locationId,
        [FromServices] DeleteLocationHandler handler,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteLocationRequest();
        var result = await handler.Handle(
            request.ToCommand(id, locationId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.ToString());
    }
    
}