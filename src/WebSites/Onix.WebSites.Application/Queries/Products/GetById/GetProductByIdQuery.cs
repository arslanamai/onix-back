using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Queries.Products.GetById;

public record GetProductByIdQuery(Guid WebSiteId, Guid ProductId) : IQuery;