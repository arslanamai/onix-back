using Onix.WebSites.Application.Queries.WebSites.GetById;

namespace Onix.WebSites.Presentation.Controllers.Requests.WebSites;

public record GetWebSiteRequest()
{
    public GetWebSiteByIdQuery ToQuery(Guid id) => new(id);
}