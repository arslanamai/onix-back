using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Queries.WebSites.GetByIdWithBlocks;

public record GetWebSiteByIdWithBlocksQuery (Guid Id) : IQuery;