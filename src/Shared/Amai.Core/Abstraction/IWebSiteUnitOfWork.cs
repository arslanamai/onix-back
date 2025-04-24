using System.Data;

namespace Amai.Core.Abstraction;

public interface IWebSiteUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}