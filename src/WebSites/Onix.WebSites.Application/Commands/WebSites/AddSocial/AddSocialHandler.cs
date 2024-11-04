using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;
using Onix.WebSites.Domain.WebSites.ValueObjects;

namespace Onix.WebSites.Application.Commands.WebSites.AddSocial;

public class AddSocialHandler
{
    private readonly ILogger<AddSocialHandler> _logger;
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IValidator<AddSocialCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public AddSocialHandler(
        ILogger<AddSocialHandler> logger,
        IWebSiteRepository webSiteRepository,
        IValidator<AddSocialCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _webSiteRepository = webSiteRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        AddSocialCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command,cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();
        var webSiteId = WebSiteId.Create(command.WebSiteId);
        
        var webSiteResult = await _webSiteRepository
            .GetById(webSiteId, cancellationToken);
        if (webSiteResult.IsFailure)
            return webSiteResult.Error.ToErrorList();

        var result = webSiteResult.Value.AddSocial(command.SocialMedia);
        if (result.IsFailure)
            return result.Error.ToErrorList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<ErrorList>();
    }
}