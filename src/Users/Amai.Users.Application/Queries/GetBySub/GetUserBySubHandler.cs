using Amai.Core.Abstraction;
using Amai.Core.Dtos;
using Amai.SharedKernel;
using Amai.Users.Application.DateBase;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Amai.Users.Application.Queries.GetBySub;

public class GetUserBySubHandler : IQueryHandlerWithResult<UserDto, GetUserBySubQuery>
{
    private readonly IReadUserDbContext _userDbContext;

    public GetUserBySubHandler(IReadUserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }
    
    public async Task<Result<UserDto, ErrorList>> Handle(
        GetUserBySubQuery query, CancellationToken cancellationToken = default)
    {
        //wow!!!!!!!
        var userDto = await _userDbContext.Users
            .FirstOrDefaultAsync(u => u.Sub == query.Sub, cancellationToken);

        if (userDto is null)
            return Errors.General.NotFound(ConstType.User).ToErrorList();

        return userDto;
    }
}