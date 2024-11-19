using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Onix.Core.Dtos;
using Onix.SharedKernel;
using Onix.WebSites.Application.Database;

namespace Onix.WebSites.Application.Queries.Categories.GetById;

public class GetCategoryByIdHandler
{
    private readonly IReadDbContext _readDbContext;

    public GetCategoryByIdHandler(
        IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<Result<CategoryDto, ErrorList>> Handle(
        GetCategoryByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var webSiteDto = await _readDbContext.WebSites
            .FirstOrDefaultAsync(w => w.Id == query.WebSiteId, cancellationToken);
        if (webSiteDto is null)
            return Errors.General.NotFound(query.WebSiteId).ToErrorList();
        
        var categoryDto = await _readDbContext.Category
            .Include(c => c.SubCategories)
            .Include(c => c.Products)
            .ThenInclude(p => p.Photos)
            .FirstOrDefaultAsync(c => c.Id == query.CategoryId, cancellationToken);
        if (categoryDto is null)
            return Errors.General.NotFound(query.CategoryId).ToErrorList();
    
        return categoryDto;
    }
}