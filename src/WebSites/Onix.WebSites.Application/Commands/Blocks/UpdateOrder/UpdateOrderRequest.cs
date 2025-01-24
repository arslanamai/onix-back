using Onix.Core.Dtos;

namespace Onix.WebSites.Application.Commands.Blocks.UpdateOrder;

public record UpdateOrderRequest(
    Guid WebSiteId, List<BlockDto> blocks);