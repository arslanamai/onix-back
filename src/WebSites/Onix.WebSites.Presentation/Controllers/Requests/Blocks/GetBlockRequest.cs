using Onix.WebSites.Application.Queries.WebSites.GetByIdWithBlocks;

namespace Onix.WebSites.Presentation.Controllers.Requests.Blocks;

public record GetBlockRequest()
{
    public GetBlocksQuery ToQuery(Guid id) => new(id);
}