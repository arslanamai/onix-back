using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Onix.Account.Infrastructure.DbContexts;
using Onix.Core.Abstraction;

namespace Onix.Account.Infrastructure;

public class AccountUnitOfWork : IAccountUnitOfWork
{
    private readonly WriteAccountDbContext _dbContexts;

    public AccountUnitOfWork(WriteAccountDbContext dbContexts)
    {
        _dbContexts = dbContexts;
    }
    
    public async Task<IDbTransaction> BeginTransaction(
        CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContexts.Database.BeginTransactionAsync(cancellationToken);
        
        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _dbContexts.SaveChangesAsync(cancellationToken);
    }
}