using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Onix.Core.Dtos;
using Onix.SharedKernel;
using Onix.WebSites.Application.Database;

namespace Onix.WebSites.Application.Queries.WebSites.GetByUrl;

public class GetWebSiteByUrlHandler
{
    private readonly IWebSiteReadDbContext _webSiteReadDbContext;

    public GetWebSiteByUrlHandler(IWebSiteReadDbContext webSiteReadDbContext)
    {
        _webSiteReadDbContext = webSiteReadDbContext;
    }
    
    public async Task<Result<WebSiteDto, Error>> Handle(
        GetWebSiteByUrlQuery query,
        CancellationToken cancellationToken = default)
    {
        var webSiteDto = await _webSiteReadDbContext.WebSites
            .FirstOrDefaultAsync(w => w.SubDamain == query.SubDomain, cancellationToken);
        
        if (webSiteDto is null)
            return Errors.General.NotFound(ConstType.WebSite);

        return webSiteDto;
    }
}