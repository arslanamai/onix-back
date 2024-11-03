using Onix.WebSites.Application.Commands.WebSites.Delete;

namespace Onix.WebSites.Presentation.Controllers.Requests.WebSites;

public record DeleteWebSiteRequest()
{
    public DeleteWebSiteCommand ToCommand(Guid id)
        => new DeleteWebSiteCommand(id);
}