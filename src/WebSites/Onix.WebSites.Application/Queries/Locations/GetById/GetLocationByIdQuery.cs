using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Queries.Locations.GetById;

public record GetLocationByIdQuery(Guid WebSiteId, Guid LocationId) : IQuery;