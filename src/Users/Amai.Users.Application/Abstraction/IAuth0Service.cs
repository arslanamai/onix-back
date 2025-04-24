using Amai.Core.Response;
using Amai.SharedKernel;
using CSharpFunctionalExtensions;

namespace Amai.Users.Application.Abstraction;

public interface IAuth0Service
{
    Task<Result<UserResponse, Error>> RegisterAsync(string email, string password);
    Task<Result<TokenResponse, Error>> LoginAsync(string email, string password);
}