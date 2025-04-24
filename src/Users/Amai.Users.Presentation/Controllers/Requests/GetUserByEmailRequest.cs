using Amai.Users.Application.Queries.GetByEmail;

namespace Amai.Users.Presentation.Controllers.Requests;

public record GetUserByEmailRequest(
    string Email)
{
    public GetUserByEmailQuery ToQuery()
        => new GetUserByEmailQuery(Email);
} 