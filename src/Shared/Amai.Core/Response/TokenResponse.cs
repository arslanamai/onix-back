using System.Text.Json.Serialization;

namespace Amai.Core.Response;

public record TokenResponse
{
    [JsonPropertyName("access_token")] 
    public string AccessToken { get; set; } = string.Empty;
    
    [JsonPropertyName("refresh_token")] 
    public string RefreshToken { get; set; } = string.Empty;
    
    [JsonPropertyName("scope")] 
    public string Scope { get; set; } = string.Empty;
    
    [JsonPropertyName("expires_in")] 
    public int Expires { get; set; }

    [JsonPropertyName("token_type")] 
    public string TokenType { get; set; } = string.Empty;
}