using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;
using Onix.WebSites.Domain.WebSites;
using Onix.WebSites.Infrastructure.DbContexts;

namespace Onix.WebSites.Infrastructure.Repositories;

public class WebSiteRepository : IWebSiteRepository
{
    private readonly WebSiteWriteDbContext _dbContext;

    public WebSiteRepository( WebSiteWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task<Result<WebSite,Error>> GetById(
        WebSiteId webSiteId, CancellationToken cancellationToken = default)
    {
        var webSite = await _dbContext.WebSites
            .FirstOrDefaultAsync(w => w.Id == webSiteId, cancellationToken );

        if (webSite is null)
            return Errors.General.NotFound(ConstType.WebSite);
        
        return webSite;
    }

    public async Task<Guid> Add(
        WebSite webSite, CancellationToken cancellationToken = default)
    {
        await _dbContext.WebSites.AddAsync(webSite, cancellationToken);

        return webSite.Id.Value;
    }

    public Guid Save(
        WebSite webSite, CancellationToken cancellationToken = default)
    {
        _dbContext.WebSites.Attach(webSite);
        return webSite.Id.Value;
    }

    public Guid Delete(
        WebSite webSite, CancellationToken cancellationToken = default)
    {
        _dbContext.WebSites.Remove(webSite);
        return webSite.Id.Value;
    }

    public async Task<Result<WebSite, Error>> GetByUrl(
        SubDomain domain, CancellationToken cancellationToken = default)
    {
        var webSite = await _dbContext.WebSites
            .FirstOrDefaultAsync(w => w.SubDomain == domain, cancellationToken );

        if (webSite is null)
            return Errors.General.NotFound(ConstType.WebSite);

        return webSite;
    }

    public async Task<Result<WebSite, Error>> GetByIdWithBlocks(
        WebSiteId id, CancellationToken cancellationToken = default)
    {
        var webSite = await _dbContext.WebSites
            .Include(w => w.Blocks)
            .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
        
        if (webSite is null)
            return Errors.General.NotFound(ConstType.WebSite);

        return webSite;
    }

    public async Task<Result<WebSite, Error>> GetByIdWithLocation(
        WebSiteId id, CancellationToken cancellationToken = default)
    {
        var webSite = await _dbContext.WebSites
            .Include(w => w.Locations)
            .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
        
        if (webSite is null)
            return Errors.General.NotFound(ConstType.WebSite);

        return webSite;
    }
}