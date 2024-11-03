using Onix.Core.Dtos;

namespace Onix.WebSites.Application.Database;

public interface IReadDbContext
{
    IQueryable<WebSiteDto> WebSites { get; }
    IQueryable<BlockDto> Blocks { get; }
    IQueryable<LocationDto> Location { get; }
    IQueryable<ProductDto> Product { get; }
    IQueryable<PhotoDto> Photo { get; }
    IQueryable<CategoryDto> Category { get; }

}