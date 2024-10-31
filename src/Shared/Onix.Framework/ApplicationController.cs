using Microsoft.AspNetCore.Mvc;
using Onix.Core.Response;

namespace Onix.Framework;

[ApiController]
public class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);
        
        return base.Ok(envelope);
    }
}