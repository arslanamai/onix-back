using Onix.Core.Dtos;

namespace Onix.WebSites.Application.Database;

public interface IWebSiteReadDbContext
{
    IQueryable<WebSiteDto> WebSites { get; }
    IQueryable<BlockDto> Blocks { get; }
    IQueryable<LocationDto> Location { get; }
    IQueryable<ProductDto> Product { get; }
    IQueryable<PhotoDto> Photo { get; }

}