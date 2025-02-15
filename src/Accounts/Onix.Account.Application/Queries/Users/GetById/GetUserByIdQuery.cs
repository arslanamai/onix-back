using Onix.Core.Abstraction;

namespace Onix.Account.Application.Queries.Users.GetById;

public record GetUserByIdQuery(Guid Id) : IQuery;