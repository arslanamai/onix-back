using Onix.WebSites.Application.Commands.Blocks.Delete;

namespace Onix.WebSites.Presentation.Controllers.Requests.Blocks;

public record DeleteBlockRequest
{
    public DeleteBlockCommand ToCommand(Guid id, Guid blockId)
        => new DeleteBlockCommand(id, blockId);
} 