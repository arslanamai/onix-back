using System.Text.Json.Serialization;

namespace Amai.Core.Response;

public record UserResponse
{
    [JsonPropertyName("_id")]
    public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    
    [JsonPropertyName("email_verified")]
    public bool EmailVerified { get; set; }
}