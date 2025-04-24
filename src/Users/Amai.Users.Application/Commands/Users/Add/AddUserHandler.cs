using Amai.Core.Abstraction;
using Amai.Core.Contracts.Users;
using Amai.Core.Extensions;
using Amai.SharedKernel;
using Amai.SharedKernel.ValueObjects;
using Amai.SharedKernel.ValueObjects.Ids;
using Amai.Users.Application.Abstraction;
using Amai.Users.Application.DateBase;
using Amai.Users.Domain.Users;
using Amai.Users.Domain.Users.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Amai.Users.Application.Commands.Users.Add;

public class AddUserHandler: ICommandHandler<string,AddUserCommand>
{
    private readonly IValidator<AddUserCommand> _validator;
    private readonly ILogger<AddUserHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IUserUnitOfWork _userUnitOfWork;
    private readonly IUserContract _userContract;
    private readonly IAuth0Service _auth0Service;

    public AddUserHandler(
        IValidator<AddUserCommand> validator,
        ILogger<AddUserHandler> logger,
        IUserRepository userRepository,
        IUserUnitOfWork userUnitOfWork,
        IUserContract userContract,
        IAuth0Service auth0Service  )
    {
        _validator = validator;
        _logger = logger;
        _userRepository = userRepository;
        _userUnitOfWork = userUnitOfWork;
        _userContract = userContract;
        _auth0Service = auth0Service;
    }

    public async Task<Result<string, ErrorList>> Handle(
        AddUserCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var userResult = await _userContract.GetUserByEmailAsync(command.Email, cancellationToken);
        if (userResult.IsSuccess)
            return Errors.Domains.AlreadyExist(ConstType.Email).ToErrorList();

        await using var transaction = await _userUnitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var email = Email.Create(command.Email).Value;
            var userId = UserId.NewId();

            var user = User.Create(
                userId,
                email,
                DateTimeOffset.UtcNow).Value;
            
            await _userRepository.Add(user, cancellationToken);
            
            var registerResult = await _auth0Service.RegisterAsync(command.Email, command.Password);
            if (registerResult.IsFailure)
                return registerResult.Error.ToErrorList();

            var sub = Sub.Create(registerResult.Value.UserId).Value;
            user.UpdateSub(sub);
            
            await _userUnitOfWork.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return registerResult.Value.Email;

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Errors.General.ErrorCode("че там").ToErrorList();
        }
        
        /*var token = await _auth0Service.LoginAsync(command.Email, command.Password);
        if (token.IsFailure)
            return token.Error.ToErrorList();

        return token.Value;*/
    }
}