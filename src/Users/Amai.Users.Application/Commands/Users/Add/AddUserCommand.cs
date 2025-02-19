using Onix.Core.Abstraction;

namespace Amai.Users.Application.Commands.Users.Add;

public record AddUserCommand(string Email) : ICommand;