namespace Onix.Core.Dtos;

public class ServiceDto
{
    public Guid Id { get; init; }
    public Guid CategoryId { get; init; }
    
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;

    public string Price { get; init; } = string.Empty;
    public int? Duration { get; init; }

    public string Link { get; init; } = string.Empty;

    public List<PhotoDto> Photos { get; init; } = [];
}