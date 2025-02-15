using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Onix.Core.Dtos;
using Onix.SharedKernel;
using Onix.WebSites.Application.Database;

namespace Onix.WebSites.Application.Queries.WebSites.GetById;

public class GetWebSiteByIdHandler
{
    private readonly IWebSiteReadDbContext _webSiteReadDbContext;

    public GetWebSiteByIdHandler(IWebSiteReadDbContext webSiteReadDbContext)
    {
        _webSiteReadDbContext = webSiteReadDbContext;
    }

    public async Task<Result<WebSiteDto, ErrorList>> Handle(
        GetWebSiteByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var webSiteDto = await _webSiteReadDbContext.WebSites
            .FirstOrDefaultAsync(w => w.Id == query.Id, cancellationToken);

        if (webSiteDto is null)
            return Errors.General.NotFound(ConstType.WebSite).ToErrorList();

        return webSiteDto;
    }
}