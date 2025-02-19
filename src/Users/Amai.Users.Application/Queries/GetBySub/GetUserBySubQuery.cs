using Onix.Core.Abstraction;

namespace Amai.Users.Application.Queries.GetBySub;

public record GetUserBySubQuery(string Sub) : IQuery;