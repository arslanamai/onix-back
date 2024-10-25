using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Queries.WebSites.GetByIdWithFavicon;

public record GetWebSiteByIdWithFaviconQuery (Guid Id) : IQuery;