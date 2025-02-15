using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Onix.Account.Application.Database;
using Onix.Core.Dtos;
using Onix.SharedKernel;

namespace Onix.Account.Application.Queries.Users.GetByEmail;

public class GetUserByEmailHandler
{
    private readonly IReadAccountDbContext _accountDbContext;

    public GetUserByEmailHandler(IReadAccountDbContext accountDbContext)
    {
        _accountDbContext = accountDbContext;
    }

    public async Task<Result<UserDto, ErrorList>> Handle(
        GetUserByEmailQuery query, CancellationToken cancellationToken = default)
    {
        var userDto = await _accountDbContext.Users
            .FirstOrDefaultAsync(u => u.Email == query.Email, cancellationToken);
        
        if(userDto is null)
            return Errors.General.NotFound(ConstType.User).ToErrorList();

        return userDto;
    }
}