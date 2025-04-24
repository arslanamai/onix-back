using Microsoft.EntityFrameworkCore.Storage;

namespace Amai.Core.Abstraction;

public interface IUserUnitOfWork
{
    Task<IDbContextTransaction> BeginTransaction(
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}