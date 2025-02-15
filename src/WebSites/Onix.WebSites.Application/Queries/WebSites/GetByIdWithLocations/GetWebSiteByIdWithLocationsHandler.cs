using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Onix.Core.Dtos;
using Onix.SharedKernel;
using Onix.WebSites.Application.Database;
using Onix.WebSites.Application.Queries.WebSites.GetById;

namespace Onix.WebSites.Application.Queries.WebSites.GetByIdWithLocations;

public class GetWebSiteByIdWithLocationsHandler
{
    private readonly IWebSiteReadDbContext _webSiteReadDbContext;

    public GetWebSiteByIdWithLocationsHandler(IWebSiteReadDbContext webSiteReadDbContext)
    {
        _webSiteReadDbContext = webSiteReadDbContext;
    }

    public async Task<Result<WebSiteDto, ErrorList>> Handle(
        GetWebSiteByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var webSiteDto = await _webSiteReadDbContext.WebSites
            .Include(w => w.Location)
            .FirstOrDefaultAsync(w => w.Id == query.Id, cancellationToken);

        if (webSiteDto is null)
            return Errors.General.NotFound(ConstType.WebSite).ToErrorList();

        return webSiteDto;
    }
}