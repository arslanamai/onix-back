using System.Net.Http.Json;
using Amai.Core.Response;
using Amai.SharedKernel;
using Amai.Users.Application.Abstraction;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;

namespace Amai.Users.Infrastructure.Services;

public class Auth0Service : IAuth0Service
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public Auth0Service(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<Result<UserResponse, Error>> RegisterAsync(string email, string password)
    {
        var request = new
        {
            client_id = _config["Auth0:ClientId"],
            email,
            password,
            connection = "amai-connection"
        };
        
        var response = await _httpClient.PostAsJsonAsync(
            $"{_config["Auth0:Domain"]}/dbconnections/signup",
            request);

        if (!response.IsSuccessStatusCode )
            return Errors.Authentication.Register();
        
        var result = await response.Content.ReadFromJsonAsync<UserResponse>();
        if (result is null)
            return Errors.General.ErrorCode();
        
        return result;
    }
    
    public async Task<Result<TokenResponse, Error>> LoginAsync(string email, string password)
    {
        var request = new
        {
            grant_type = "password",
            client_id = _config["Auth0:ClientId"],
            client_secret = _config["Auth0:ClientSecret"],
            username = email,
            password,
            audience = _config["Auth0:Audience"],
            scope = "offline_access",
            connection = "amai-connection"
        };

        var response = await _httpClient.PostAsJsonAsync(
            $"{_config["Auth0:Domain"]}/oauth/token",
            request);

        if (!response.IsSuccessStatusCode)
            return Errors.Authentication.Login();
        
        var results = await response.Content.ReadAsStringAsync();

        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        if (result is null)
            return Errors.Authentication.TokenError();
        
        return result;
    }
}