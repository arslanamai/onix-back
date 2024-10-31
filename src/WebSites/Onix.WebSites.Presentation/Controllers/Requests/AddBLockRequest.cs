using Onix.WebSites.Application.Commands.Blocks.Add;

namespace Onix.WebSites.Presentation.Controllers.Requests;

public record AddBLockRequest(
    string Code)
{
    public AddBlockCommand ToCommand(Guid webSiteId)
        => new (webSiteId, Code);
}