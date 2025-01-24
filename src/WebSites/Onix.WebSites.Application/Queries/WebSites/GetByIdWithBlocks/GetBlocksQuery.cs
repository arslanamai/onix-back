using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Queries.WebSites.GetByIdWithBlocks;

public record GetBlocksQuery (Guid Id) : IQuery;