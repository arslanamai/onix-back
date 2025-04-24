using Amai.Users.Application.Queries.GetById;

namespace Amai.Users.Presentation.Controllers.Requests;

public record GetUserRequest(
    Guid Id)
{
    public GetUserByIdQuery ToQuery()
        => new GetUserByIdQuery(Id);
} 