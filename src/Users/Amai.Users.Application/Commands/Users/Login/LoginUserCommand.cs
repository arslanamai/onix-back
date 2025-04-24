using Amai.Core.Abstraction;

namespace Amai.Users.Application.Commands.Users.Login;

public record LoginUserCommand(string Email, string Password) : ICommand;