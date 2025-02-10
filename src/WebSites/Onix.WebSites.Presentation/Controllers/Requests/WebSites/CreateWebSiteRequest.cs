using Onix.WebSites.Application.Commands.WebSites.Create;

namespace Onix.WebSites.Presentation.Controllers.Requests.WebSites;

public record CreateWebSiteRequest(
    string SubDomain,
    string Name)
{
    public CreateWebSiteCommand ToCommand()
        => new(SubDomain, Name);
}