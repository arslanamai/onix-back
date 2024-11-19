using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Queries.Categories.GetById;

public record GetCategoryByIdQuery(
    Guid WebSiteId,
    Guid CategoryId) : IQuery;