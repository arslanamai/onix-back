using Amai.Core.Abstraction;
using Amai.Core.Dtos;
using Amai.SharedKernel;
using Amai.Users.Application.DateBase;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Amai.Users.Application.Queries.GetByEmail;

public class GetUserByEmailHandler : IQueryHandlerWithResult<UserDto, GetUserByEmailQuery>
{
    private readonly IReadUserDbContext _userDbContext;

    public GetUserByEmailHandler(IReadUserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }
    
    public async Task<Result<UserDto, ErrorList>> Handle(
        GetUserByEmailQuery query, CancellationToken cancellationToken = default)
    {
        var userDto = await _userDbContext.Users
            .FirstOrDefaultAsync(u => u.Email == query.Email, cancellationToken);

        if (userDto is null)
            return Errors.General.NotFound(ConstType.User).ToErrorList();

        return userDto;
    }
}