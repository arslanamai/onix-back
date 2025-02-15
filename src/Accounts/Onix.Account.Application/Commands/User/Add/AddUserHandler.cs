using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Account.Application.Database;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;

namespace Onix.Account.Application.Commands.User.Add;

public class AddUserHandler
{
    private readonly IValidator<AddUserCommand> _validator;
    private readonly ILogger<AddUserHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IAccountUnitOfWork _accountUnitOfWork;

    public AddUserHandler(
        IValidator<AddUserCommand> validator,
        ILogger<AddUserHandler> logger,
        IUserRepository userRepository,
        IAccountUnitOfWork accountUnitOfWork)
    {
        _validator = validator;
        _logger = logger;
        _userRepository = userRepository;
        _accountUnitOfWork = accountUnitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddUserCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();
        
        
    }
}