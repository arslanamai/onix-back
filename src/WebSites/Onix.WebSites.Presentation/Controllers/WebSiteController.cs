using Microsoft.AspNetCore.Mvc;
using Onix.Framework;
using Onix.WebSites.Application.Commands.WebSites.AddFaq;
using Onix.WebSites.Application.Commands.WebSites.AddSocial;
using Onix.WebSites.Application.Commands.WebSites.Create;
using Onix.WebSites.Application.Commands.WebSites.Delete;
using Onix.WebSites.Application.Commands.WebSites.Update;
using Onix.WebSites.Application.Commands.WebSites.UpdateContact;
using Onix.WebSites.Application.Queries.WebSites.GetById;
using Onix.WebSites.Presentation.Controllers.Requests.WebSites;

namespace Onix.WebSites.Presentation.Controllers;

public class WebSiteController : ApplicationController
{
    [HttpGet("/website/{id:guid}")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid id,
        [FromServices] GetWebSiteByIdHandle handler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetWebSiteRequest();
        var result = await handler.Handle(query.ToQuery(id), cancellationToken);

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
        [FromServices] UpdateWebSiteHandler handler,
        [FromBody] UpdateWebSiteRequest request,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.ToString());
    }

    [HttpDelete("/website/{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteWebSiteHandler handler,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteWebSiteRequest();
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.ToString());
    }

    //contacts
    [HttpPost("/website/{id:guid}/contacts")]
    public async Task<IActionResult> AddContact(
        [FromRoute] Guid id,
        [FromServices] UpdateContactHandler handler,
        [FromBody] UpdateContactRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.ToString());
    }
    
    [HttpPost("/website/{id:guid}/socials")]
    public async Task<IActionResult> AddSocial(
        [FromRoute] Guid id,
        [FromServices] AddSocialHandler handler,
        [FromBody] AddSocialRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(
            request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.ToString());
    }
    
    [HttpPost("/website/{id:guid}/faqs")]
    public async Task<IActionResult> AddSocial(
        [FromRoute] Guid id,
        [FromServices] AddFaqHandler handler,
        [FromBody] AddFaqRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(
            request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.ToString());
    }
}