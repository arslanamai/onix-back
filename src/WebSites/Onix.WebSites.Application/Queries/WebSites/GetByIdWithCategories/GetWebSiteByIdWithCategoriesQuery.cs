using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Queries.WebSites.GetByIdWithCategories;

public record GetWebSiteByIdWithCategoriesQuery (Guid Id) : IQuery;