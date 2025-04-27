using Amai.Core.Abstraction;
using Amai.Core.Contracts.Users;
using Amai.Core.Extensions;
using Amai.Core.Response;
using Amai.SharedKernel;
using Amai.SharedKernel.ValueObjects;
using Amai.SharedKernel.ValueObjects.Ids;
using Amai.Users.Application.Abstraction;
using Amai.Users.Application.Commands.Users.Add;
using Amai.Users.Application.DateBase;
using Amai.Users.Domain.Users;
using Amai.Users.Domain.Users.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Amai.Users.Application.Commands.Users.Login;

public class LoginUserHandler: ICommandHandler<TokenResponse,AddUserCommand>
{
    private readonly IValidator<AddUserCommand> _validator;
    private readonly ILogger<AddUserHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IUserUnitOfWork _userUnitOfWork;
    private readonly IUserContract _userContract;
    private readonly IAuthService _authService;

    public LoginUserHandler(
        IValidator<AddUserCommand> validator,
        ILogger<AddUserHandler> logger,
        IUserRepository userRepository,
        IUserUnitOfWork userUnitOfWork,
        IUserContract userContract,
        IAuthService authService  )
    {
        _validator = validator;
        _logger = logger;
        _userRepository = userRepository;
        _userUnitOfWork = userUnitOfWork;
        _userContract = userContract;
        _authService = authService;
    }

    public async Task<Result<TokenResponse, ErrorList>> Handle(
        AddUserCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var tokenResult = await _authService.LoginAsync(command.Email, command.Password);
        if (tokenResult.IsFailure)
            return tokenResult.Error.ToErrorList();
        
        return tokenResult.Value;
    }
}