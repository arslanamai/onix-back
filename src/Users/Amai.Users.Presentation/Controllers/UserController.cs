using Amai.Framework;
using Amai.Users.Application.Commands.Users.Add;
using Amai.Users.Application.Commands.Users.Login;
using Amai.Users.Application.Queries.GetByEmail;
using Amai.Users.Presentation.Controllers.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amai.Users.Presentation.Controllers;

public class UserController : ApplicationController
{
    [HttpPost("/register")]
    public async Task<IActionResult> Register(
        [FromServices] AddUserHandler handler,
        [FromBody] AddUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpPost("/login")]
    public async Task<IActionResult> Login(
        [FromServices] LoginUserHandler handler,
        [FromBody] LoginUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpGet("/user")]
    public async Task<IActionResult> CheckEmail(
        [FromServices] GetUserByEmailHandler handler,
        [FromQuery] GetUserByEmailQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(query, cancellationToken);

        if (result.IsFailure)
            return Ok(false);
            //return result.Error.ToResponse();

        return Ok(true);
    }

    [Authorize]
    [HttpPost("/test")]
    public IActionResult Test()
    {
        return Ok();
    }
}