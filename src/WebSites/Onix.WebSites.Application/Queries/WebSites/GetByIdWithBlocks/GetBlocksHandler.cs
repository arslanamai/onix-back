using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Onix.Core.Dtos;
using Onix.SharedKernel;
using Onix.WebSites.Application.Database;

namespace Onix.WebSites.Application.Queries.WebSites.GetByIdWithBlocks;

public class GetBlocksHandler
{
    private readonly IWebSiteReadDbContext _webSiteReadDbContext;

    public GetBlocksHandler(IWebSiteReadDbContext webSiteReadDbContext)
    {
        _webSiteReadDbContext = webSiteReadDbContext;
    } 

    public async Task<Result<IReadOnlyList<BlockDto>, ErrorList>> Handle(
        GetBlocksQuery query,
        CancellationToken cancellationToken = default)
    {
        var webSiteDto = await _webSiteReadDbContext.WebSites
            .Include(w => w.Blocks)
            .FirstOrDefaultAsync(w => w.Id == query.Id, cancellationToken);

        if (webSiteDto is null)
            return Errors.General.NotFound(ConstType.WebSite).ToErrorList();

        return webSiteDto.Blocks.ToList();
    }
}