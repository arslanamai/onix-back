using Amai.Core.Abstraction;

namespace Amai.Users.Application.Commands.Users.Delete;

public record DeleteUserCommand(Guid Id) : ICommand;