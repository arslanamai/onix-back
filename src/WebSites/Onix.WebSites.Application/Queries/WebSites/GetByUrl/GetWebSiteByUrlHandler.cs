using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Onix.Core.Dtos;
using Onix.SharedKernel;
using Onix.WebSites.Application.Database;

namespace Onix.WebSites.Application.Queries.WebSites.GetByUrl;

public class GetWebSiteByUrlHandler
{
    private readonly IReadDbContext _readDbContext;

    public GetWebSiteByUrlHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<Result<WebSiteDto, Error>> Handle(
        GetWebSiteByUrlQuery query,
        CancellationToken cancellationToken = default)
    {
        var webSiteDto = await _readDbContext.WebSites
            .FirstOrDefaultAsync(w => w.SubDamain == query.SubDomain, cancellationToken);
        
        if (webSiteDto is null)
            return Errors.General.NotFound(ConstType.WebSite);

        return webSiteDto;
    }
}