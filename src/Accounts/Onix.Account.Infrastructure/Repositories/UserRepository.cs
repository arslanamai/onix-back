using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Onix.Account.Application.Database;
using Onix.Account.Domain.Accounts;
using Onix.Account.Infrastructure.DbContexts;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;

namespace Onix.Account.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly WriteAccountDbContext _dbContext;

    public UserRepository(WriteAccountDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<User,Error>> GetById(
        UserId userId, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(w => w.Id == userId, cancellationToken );

        if (user is null)
            return Errors.General.NotFound(ConstType.User);
        
        return user;
    }
    
    public async Task<Guid> Add(
        User user, CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);

        return user.Id.Value;
    }

    public Guid Save(
        User user, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Attach(user);
        return user.Id.Value;
    }

    public Guid Delete(
        User user, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Remove(user);
        return user.Id.Value;
    }
}