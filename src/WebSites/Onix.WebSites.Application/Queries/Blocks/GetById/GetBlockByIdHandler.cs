using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Onix.Core.Dtos;
using Onix.SharedKernel;
using Onix.WebSites.Application.Database;

namespace Onix.WebSites.Application.Queries.Blocks.GetById;

public class GetBlockByIdHandler
{
    private readonly IReadDbContext _readDbContext;

    public GetBlockByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<BlockDto, ErrorList>> Handle(
        GetBlockByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var webSiteDto = await _readDbContext.WebSites
            .FirstOrDefaultAsync(w => w.Id == query.WebSiteId, cancellationToken);

        if (webSiteDto is null)
            return Errors.General.NotFound(ConstType.WebSite).ToErrorList();
        
        var blockDto = webSiteDto.Blocks
            .FirstOrDefault(b => b.Id == query.BlockId);

        if (blockDto is null)
            return Errors.General.NotFound(ConstType.Block).ToErrorList();

        return blockDto;
    }
}