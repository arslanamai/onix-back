using Amai.Users.Domain.Users;
using CSharpFunctionalExtensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;

namespace Amai.Users.Application.DateBase;

public interface IUserRepository
{
    Task<Result<User,Error>> GetById(
        UserId userId, CancellationToken cancellationToken = default);

    Task<Guid> Add(
        User user, CancellationToken cancellationToken = default);

    Guid Save(
        User user, CancellationToken cancellationToken = default);

    Guid Delete(
        User user, CancellationToken cancellationToken = default);
}