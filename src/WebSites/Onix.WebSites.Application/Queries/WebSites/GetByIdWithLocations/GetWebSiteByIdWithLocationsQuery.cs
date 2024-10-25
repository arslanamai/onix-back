using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Queries.WebSites.GetByIdWithLocations;

public record GetWebSiteByIdWithLocationsQuery (Guid Id) : IQuery;