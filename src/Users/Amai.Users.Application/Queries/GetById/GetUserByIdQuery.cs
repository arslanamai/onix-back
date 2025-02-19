using Onix.Core.Abstraction;

namespace Amai.Users.Application.Queries.GetById;

public record GetUserByIdQuery(Guid Id) : IQuery;