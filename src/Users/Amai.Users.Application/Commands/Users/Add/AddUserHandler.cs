using Amai.Users.Application.DateBase;
using Amai.Users.Domain.Users;
using Amai.Users.Domain.Users.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Contracts.Users;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;

namespace Amai.Users.Application.Commands.Users.Add;

public class AddUserHandler
{
    private readonly IValidator<AddUserCommand> _validator;
    private readonly ILogger<AddUserHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IUserUnitOfWork _userUnitOfWork;
    private readonly IUserContract _userContract;

    public AddUserHandler(
        IValidator<AddUserCommand> validator,
        ILogger<AddUserHandler> logger,
        IUserRepository userRepository,
        IUserUnitOfWork userUnitOfWork,
        IUserContract userContract)
    {
        _validator = validator;
        _logger = logger;
        _userRepository = userRepository;
        _userUnitOfWork = userUnitOfWork;
        _userContract = userContract;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddUserCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var userResult = await _userContract
            .GetUserByEmailAsync(command.Email, cancellationToken);
        if (userResult.IsSuccess)
            return Errors.Domains.AlreadyExist(ConstType.Email).ToErrorList();

        var email = Email.Create(command.Email).Value;
        var userId = UserId.NewId();
        var sub = Sub.Create("").Value;//временно глушилка

        var user = User.Create(
            userId,
            email,
            sub,
            DateTimeOffset.UtcNow).Value;

        await _userRepository.Add(user, cancellationToken);

        await _userUnitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id.Value;
    }
}