using Amai.Core.Abstraction;

namespace Amai.Users.Application.Commands.Users.Add;

public record AddUserCommand(string Email, string Password) : ICommand;