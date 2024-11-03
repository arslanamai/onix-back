using Onix.WebSites.Application.Commands.Blocks.Update;
using Onix.WebSites.Domain.WebSites.ValueObjects;

namespace Onix.WebSites.Presentation.Controllers.Requests.Blocks;

public record UpdateBlockRequest(
    string Code)
{
    public UpdateBlockCommand ToCommand(Guid id, Guid blockId) =>
        new UpdateBlockCommand(id, blockId,Code);
}