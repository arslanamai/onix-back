namespace Amai.Core.Dtos;

public class UserDto
{
    public Guid Id { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Sub { get; init; } = string.Empty;
}