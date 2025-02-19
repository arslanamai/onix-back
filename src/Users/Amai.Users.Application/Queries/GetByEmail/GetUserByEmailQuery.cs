using Onix.Core.Abstraction;

namespace Amai.Users.Application.Queries.GetByEmail;

public record GetUserByEmailQuery(string Email) : IQuery;