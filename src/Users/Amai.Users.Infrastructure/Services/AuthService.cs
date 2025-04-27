using System.Net.Http.Json;
using System.Text.Json;
using Amai.Core.Response;
using Amai.SharedKernel;
using Amai.Users.Application.Abstraction;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;

namespace Amai.Users.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _domain;
    private readonly string _audience;
    private readonly string _connection;
    private const string GRANT_TYPE = "password";
    private const string SCOPE = "offline_access";

    public AuthService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;

        _clientId = _config["Auth0:ClientId"] ?? throw new InvalidOperationException("ClientId not configured");
        _clientSecret = _config["Auth0:ClientSecret"] ?? throw new InvalidOperationException("ClientSecret not configured");
        _domain = _config["Auth0:Domain"] ?? throw new InvalidOperationException("Domain not configured");
        _audience = _config["Auth0:Audience"] ?? throw new InvalidOperationException("Audience not configured");
        _connection = _config["Auth0:Connection"] ?? throw new InvalidOperationException("Connection not configured");
    }

    public async Task<Result<UserResponse, Error>> RegisterAsync(string email, string password)
    {
        var request = new
        {
            ClientId = _clientId,
            Email = email,
            Password = password,
            Connection = _connection
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_domain}/dbconnections/signup", request);

            if (!response.IsSuccessStatusCode)
            {
                var errorDetail = await ReadErrorResponse(response);
                return Errors.General.Custom(response.StatusCode.ToString(),errorDetail);
            }

            var user = await ReadJsonSafe<UserResponse>(response);
            if (user is null)
                return Errors.General.JsonParseError();

            return user;
        }
        catch (Exception ex)
        {
            return Errors.General.Unexpected(ex.Message);
        }
    }

    public async Task<Result<TokenResponse, Error>> LoginAsync(string email, string password)
    {
        var request = new
        {
            GrantType = GRANT_TYPE,
            ClientId = _clientId,
            ClientSecret = _clientSecret,
            Username = email,
            Password = password,
            Audience = _audience,
            Scope = SCOPE,
            Connection = _connection
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_domain}/oauth/token", request);

            if (!response.IsSuccessStatusCode)
            {
                var errorDetail = await ReadErrorResponse(response);
                return Errors.General.Custom(response.StatusCode.ToString(),errorDetail);
            }

            var token = await ReadJsonSafe<TokenResponse>(response);
            if (token is null)
                return Errors.Authentication.TokenError();

            return token;
        }
        catch (Exception ex)
        {
            return Errors.General.Unexpected(ex.Message);
        }
    }

    private static async Task<string> ReadErrorResponse(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        return string.IsNullOrWhiteSpace(content) ? "Unknown error" : content;
    }

    private static async Task<T?> ReadJsonSafe<T>(HttpResponseMessage response)
    {
        try
        {
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (JsonException)
        {
            return default;
        }
    }
}