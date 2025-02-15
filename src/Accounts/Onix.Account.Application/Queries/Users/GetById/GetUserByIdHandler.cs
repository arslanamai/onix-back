using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Onix.Account.Application.Database;
using Onix.Core.Dtos;
using Onix.SharedKernel;

namespace Onix.Account.Application.Queries.Users.GetById;

public class GetUserByIdHandler
{
    private readonly IReadAccountDbContext _accountDbContext;

    public GetUserByIdHandler(IReadAccountDbContext accountDbContext)
    {
        _accountDbContext = accountDbContext;
    }

    public async Task<Result<UserDto, ErrorList>> Handle(
        GetUserByIdQuery query, CancellationToken cancellationToken = default)
    {
        var userDto = await _accountDbContext.Users
            .FirstOrDefaultAsync(u => u.Id == query.Id, cancellationToken);
        
        if(userDto is null)
            return Errors.General.NotFound(ConstType.User).ToErrorList();

        return userDto;
    }
}