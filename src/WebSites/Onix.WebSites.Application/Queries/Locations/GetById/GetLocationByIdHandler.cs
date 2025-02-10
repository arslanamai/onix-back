using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Onix.Core.Dtos;
using Onix.SharedKernel;
using Onix.WebSites.Application.Database;
using Onix.WebSites.Application.Queries.Blocks.GetById;

namespace Onix.WebSites.Application.Queries.Locations.GetById;

public class GetLocationByIdHandler
{
    private readonly IReadDbContext _readDbContext;

    public GetLocationByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<LocationDto, ErrorList>> Handle(
        GetLocationByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var webSiteDto = await _readDbContext.WebSites
            .FirstOrDefaultAsync(w => w.Id == query.WebSiteId, cancellationToken);

        if (webSiteDto is null)
            return Errors.General.NotFound(ConstType.WebSite).ToErrorList();
        
        var locationDto = webSiteDto.Location
            .FirstOrDefault(b => b.Id == query.LocationId);

        if (locationDto is null)
            return Errors.General.NotFound(ConstType.Location).ToErrorList();

        return locationDto;
    }
}