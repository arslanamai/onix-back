namespace Onix.Core.Dtos;

public class WebSiteDto
{
    public Guid Id { get; init; }
    
    public string Url { get; init; } = string.Empty;
    public bool ShowStatus { get; init; }
    
    public string Name { get; init; } = string.Empty;
    
    public string ColorScheme { get; init; } = string.Empty;
    public string ButtonStyle { get; init; } = string.Empty;
    public string Font { get; init; } = string.Empty;
    
    public string? Phone { get; init; } = string.Empty;
    public string? Email { get; init; } = string.Empty;

    public IReadOnlyList<FaviconDto> Favicon { get; init; } = [];
    public IReadOnlyList<SocialMediaDto> SocialMedias { get; init; } = [];
    public IReadOnlyList<FaqDto> Faqs { get; init; } = [];
    public IReadOnlyList<BlockDto> Blocks { get; init; } = [];
    public IReadOnlyList<CategoryDto> Categories { get; init; } = [];
    public IReadOnlyList<LocationDto> Location { get; init; } = [];
}