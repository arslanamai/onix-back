namespace Onix.Core.Dtos;

public class WebSiteDto
{
    public Guid Id { get; init; }
    public string SubDamain { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public DateTimeOffset CreatedDate { get; init; }
    public bool IsPublish { get; init; }
    
    public List<BlockDto> Blocks { get; init; } = [];
    public List<ProductDto> Products { get; init; } = [];
    public List<LocationDto> Location { get; init; } = [];
}