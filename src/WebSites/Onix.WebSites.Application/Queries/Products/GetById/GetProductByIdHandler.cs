using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Onix.Core.Dtos;
using Onix.SharedKernel;
using Onix.WebSites.Application.Database;
using Onix.WebSites.Application.Queries.Locations.GetById;

namespace Onix.WebSites.Application.Queries.Products.GetById;

public class GetProductByIdHandler
{
    private readonly IReadDbContext _readDbContext;

    public GetProductByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<ProductDto, ErrorList>> Handle(
        GetProductByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var webSiteDto = await _readDbContext.WebSites
            .FirstOrDefaultAsync(w => w.Id == query.WebSiteId, cancellationToken);

        if (webSiteDto is null)
            return Errors.General.NotFound(ConstType.WebSite).ToErrorList();
        
        var productDto = webSiteDto.Products
            .FirstOrDefault(b => b.Id == query.ProductId);

        if (productDto is null)
            return Errors.General.NotFound(ConstType.Product).ToErrorList();

        return productDto;
    }
}