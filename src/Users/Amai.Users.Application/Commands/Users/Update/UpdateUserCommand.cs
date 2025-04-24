using Amai.Core.Abstraction;

namespace Amai.Users.Application.Commands.Users.Update;

public record UpdateUserCommand(Guid Id, string Email) : ICommand;