using Onix.WebSites.Application.Commands.Blocks.Add;

namespace Onix.WebSites.Presentation.Controllers.Requests.Blocks;

public record AddBlockRequest(
    string Code)
{
    public AddBlockCommand ToCommand(Guid webSiteId)
        => new (webSiteId, Code);
}