using CSharpFunctionalExtensions;
using Onix.Core.Dtos;
using Onix.SharedKernel;

namespace Onix.Core.Contracts.Users;

public interface IUserContract
{
    Task<Result<UserDto, ErrorList>> GetUserByIdAsync(
        Guid id, CancellationToken cancellationToken = default);

    Task<Result<UserDto, ErrorList>> GetUserByEmailAsync(
        string email, CancellationToken cancellationToken = default);

    Task<Result<UserDto, ErrorList>> GetUserBySubAsync(
        string sub, CancellationToken cancellationToken = default);
}