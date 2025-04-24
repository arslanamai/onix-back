using Amai.Users.Application.Commands.Users.Add;

namespace Amai.Users.Presentation.Controllers.Requests;

public record LoginUserRequest(string Email, string Password)
{
    public AddUserCommand ToCommand()
        => new AddUserCommand(Email, Password);
}