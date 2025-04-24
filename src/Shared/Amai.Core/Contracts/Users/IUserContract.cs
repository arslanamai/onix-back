using Amai.Core.Dtos;
using Amai.SharedKernel;
using CSharpFunctionalExtensions;

namespace Amai.Core.Contracts.Users;

public interface IUserContract
{
    Task<Result<UserDto, ErrorList>> GetUserByIdAsync(
        Guid id, CancellationToken cancellationToken = default);

    Task<Result<UserDto, ErrorList>> GetUserByEmailAsync(
        string email, CancellationToken cancellationToken = default);

    Task<Result<UserDto, ErrorList>> GetUserBySubAsync(
        string sub, CancellationToken cancellationToken = default);
}