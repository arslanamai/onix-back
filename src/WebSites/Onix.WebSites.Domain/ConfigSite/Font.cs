using CSharpFunctionalExtensions;

namespace Onix.WebSites.Domain.ConfigSite;

public record Font
{
    private Font(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<Font> Create(string value)
    {
        return new Font(value);
    }
}