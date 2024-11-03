using Onix.WebSites.Application.Commands.WebSites.Update;

namespace Onix.WebSites.Presentation.Controllers.Requests.WebSites;

public record UpdateWebSiteRequest(
    string Url,
    string Name)
{
    public UpdateWebSiteCommand ToCommand(Guid id) 
        => new(id ,Url, Name);
}