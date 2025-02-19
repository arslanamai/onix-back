namespace Onix.Core.Dtos;

public class ProductDto
{
    public Guid Id { get; init; }
    //public Guid WebSiteId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public IReadOnlyList<PhotoDto> Photos { get; init; } = [];
}