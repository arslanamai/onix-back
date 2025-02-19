using Amai.Users.Application.DateBase;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Onix.Core.Abstraction;
using Onix.Core.Dtos;
using Onix.SharedKernel;

namespace Amai.Users.Application.Queries.GetById;


public class GetUserByIdHandler : IQueryHandlerWithResult<UserDto, GetUserByIdQuery>
{
    private readonly IReadUserDbContext _userDbContext;

    public GetUserByIdHandler(IReadUserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task<Result<UserDto, ErrorList>> Handle(
        GetUserByIdQuery query, CancellationToken cancellationToken = default)
    {
        var userDto = await _userDbContext.Users
            .FirstOrDefaultAsync(u => u.Id == query.Id, cancellationToken);

        if (userDto is null)
            return Errors.General.NotFound(ConstType.User).ToErrorList();

        return userDto;
    }
}