using Amai.Core.Response;
using Microsoft.AspNetCore.Mvc;

namespace Amai.Framework;

[ApiController]
public class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);
        
        return base.Ok(envelope);
    }
}