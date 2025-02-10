using Onix.WebSites.Application.Commands.WebSites.Update;

namespace Onix.WebSites.Presentation.Controllers.Requests.WebSites;

public record UpdateWebSiteRequest(
    string SubDomain,
    string Name)
{
    public UpdateWebSiteCommand ToCommand(Guid id) 
        => new(id ,SubDomain, Name);
}