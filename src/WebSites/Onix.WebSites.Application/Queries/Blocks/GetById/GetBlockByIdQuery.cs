using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Queries.Blocks.GetById;

public record GetBlockByIdQuery(Guid WebSiteId, Guid BlockId) : IQuery;