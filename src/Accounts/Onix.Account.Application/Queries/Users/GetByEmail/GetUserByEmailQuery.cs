using Onix.Core.Abstraction;

namespace Onix.Account.Application.Queries.Users.GetByEmail;

public record GetUserByEmailQuery(string Email) : IQuery;