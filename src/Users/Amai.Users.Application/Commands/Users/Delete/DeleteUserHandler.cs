using Amai.Core.Abstraction;
using Amai.Core.Extensions;
using Amai.SharedKernel;
using Amai.SharedKernel.ValueObjects.Ids;
using Amai.Users.Application.DateBase;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Amai.Users.Application.Commands.Users.Delete;

public class DeleteUserHandler: ICommandHandler<DeleteUserCommand>
{
    private readonly IValidator<DeleteUserCommand> _validator;
    private readonly ILogger<DeleteUserHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IUserUnitOfWork _userUnitOfWork;

    public DeleteUserHandler(
        IValidator<DeleteUserCommand> validator,
        ILogger<DeleteUserHandler> logger,
        IUserRepository userRepository,
        IUserUnitOfWork userUnitOfWork)
    {
        _validator = validator;
        _logger = logger;
        _userRepository = userRepository;
        _userUnitOfWork = userUnitOfWork;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        DeleteUserCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var userId = UserId.Create(command.Id);
        
        var userResult = await _userRepository
            .GetById(userId, cancellationToken);
        if (userResult.IsFailure)
            return Errors.General.NotFound(ConstType.User).ToErrorList();

        _userRepository.Delete(userResult.Value, cancellationToken);
        
        await _userUnitOfWork.SaveChangesAsync(cancellationToken);
        
        return UnitResult.Success<ErrorList>();
    }
}