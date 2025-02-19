using Amai.Users.Application.Queries.GetByEmail;
using Amai.Users.Application.Queries.GetById;
using Amai.Users.Application.Queries.GetBySub;
using CSharpFunctionalExtensions;
using Onix.Core.Abstraction;
using Onix.Core.Contracts.Users;
using Onix.Core.Dtos;
using Onix.SharedKernel;

namespace Amai.Users.Contract.Contracts;

public class UserContract : IUserContract
{
    private readonly IQueryHandlerWithResult<UserDto, GetUserByIdQuery> _getUserByIdHandler;
    private readonly IQueryHandlerWithResult<UserDto, GetUserByEmailQuery> _getUserByEmailHandler;
    private readonly IQueryHandlerWithResult<UserDto, GetUserBySubQuery> _getUserBySubHandler;

    public UserContract(
        IQueryHandlerWithResult<UserDto, GetUserByIdQuery> getUserByIdHandler,
        IQueryHandlerWithResult<UserDto, GetUserByEmailQuery> getUserByEmailHandler,
        IQueryHandlerWithResult<UserDto, GetUserBySubQuery> getUserBySubHandler)
    {
        _getUserByIdHandler = getUserByIdHandler;
        _getUserByEmailHandler = getUserByEmailHandler;
        _getUserBySubHandler = getUserBySubHandler;
    }
    
    public async Task<Result<UserDto, ErrorList>> GetUserByIdAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetUserByIdQuery(id);
        return await _getUserByIdHandler.Handle(query, cancellationToken);
    }
    
    public async Task<Result<UserDto, ErrorList>> GetUserByEmailAsync(
        string email, CancellationToken cancellationToken = default)
    {
        var query = new GetUserByEmailQuery(email);
        return await _getUserByEmailHandler.Handle(query, cancellationToken);
    }
    
    public async Task<Result<UserDto, ErrorList>> GetUserBySubAsync(
        string sub, CancellationToken cancellationToken = default)
    {
        var query = new GetUserBySubQuery(sub);
        return await _getUserBySubHandler.Handle(query, cancellationToken);
    }
}