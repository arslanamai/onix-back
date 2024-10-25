using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Commands.Blocks.Delete;

public record DeleteBlockCommand(
    Guid WebSiteId,
    Guid BlockId) : ICommand;