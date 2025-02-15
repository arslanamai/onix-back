using System.Data;

namespace Onix.Core.Abstraction;

public interface IAccountUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}