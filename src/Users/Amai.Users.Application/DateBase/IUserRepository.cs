using Amai.SharedKernel;
using Amai.SharedKernel.ValueObjects.Ids;
using Amai.Users.Domain.Users;
using CSharpFunctionalExtensions;

namespace Amai.Users.Application.DateBase;

public interface IUserRepository
{
    Task<Result<User, Error>> GetById(
        UserId userId, CancellationToken cancellationToken = default);

    Task<Guid> Add(
        User user, CancellationToken cancellationToken = default);

    Guid Save(
        User user, CancellationToken cancellationToken = default);

    Guid Delete(
        User user, CancellationToken cancellationToken = default);
}