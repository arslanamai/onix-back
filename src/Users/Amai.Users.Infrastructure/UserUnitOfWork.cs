using System.Data;
using Amai.Core.Abstraction;
using Amai.Users.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;
namespace Amai.Users.Infrastructure;

public class UserUnitOfWork : IUserUnitOfWork
{
    private readonly WriteUserDbContext _dbContexts;

    public UserUnitOfWork(WriteUserDbContext dbContexts)
    {
        _dbContexts = dbContexts;
    }
    
    public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContexts.Database.BeginTransactionAsync(cancellationToken);
    
        return transaction;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _dbContexts.SaveChangesAsync(cancellationToken);
    }
}