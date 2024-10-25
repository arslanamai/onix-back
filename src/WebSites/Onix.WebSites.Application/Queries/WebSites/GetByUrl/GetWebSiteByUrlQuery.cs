using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Queries.WebSites.GetByUrl;

public record GetWebSiteByUrlQuery(string Url) : IQuery;