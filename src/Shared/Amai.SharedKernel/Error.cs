namespace Amai.SharedKernel;

public sealed record Error
{
    private const string SEPARATOR = "||";

    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }
    public string? InvalidField { get; }

    private Error(string code, string message, ErrorType type, string? invalidField = null)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidField = invalidField;
    }

    // Factory methods
    public static Error Validation(string code, string message, string? invalidField = null) =>
        new(code, message, ErrorType.Validation, invalidField);

    public static Error NotFound(string code, string message) =>
        new(code, message, ErrorType.NotFound);

    public static Error Failure(string code, string message) =>
        new(code, message, ErrorType.Failure);

    public static Error Conflict(string code, string message) =>
        new(code, message, ErrorType.Conflict);

    // Serialize for logging or transport
    public string Serialize() =>
        string.Join(SEPARATOR, Code, Message, Type, InvalidField ?? string.Empty);

    public static Error Deserialize(string serialized)
    {
        var parts = serialized.Split(SEPARATOR);

        if (parts.Length < 3)
            throw new ArgumentException("Invalid serialized format", nameof(serialized));

        if (!Enum.TryParse(parts[2], out ErrorType type))
            throw new ArgumentException("Invalid error type", nameof(serialized));

        return new Error(
            parts[0],
            parts[1],
            type,
            parts.Length > 3 && !string.IsNullOrWhiteSpace(parts[3]) ? parts[3] : null
        );
    }
    
    public ErrorList ToErrorList() => new([this]);
}

public enum ErrorType
{
    Validation,
    NotFound,
    Failure,
    Conflict
}