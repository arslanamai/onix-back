using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;

namespace Onix.WebSites.Application.Commands.WebSites.Delete;

public class DeleteWebSiteHandler
{
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IValidator<DeleteWebSiteCommand> _validator;
    private readonly IWebSiteUnitOfWork _webSiteUnitOfWork;
    private readonly ILogger<DeleteWebSiteCommand> _logger;

    public DeleteWebSiteHandler(
        IWebSiteRepository webSiteRepository,
        IValidator<DeleteWebSiteCommand> validator,
        IWebSiteUnitOfWork webSiteUnitOfWork,
        ILogger<DeleteWebSiteCommand> logger)
    {
        _webSiteRepository = webSiteRepository;
        _validator = validator;
        _webSiteUnitOfWork = webSiteUnitOfWork;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        DeleteWebSiteCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var webSiteId = WebSiteId.Create(command.WebSiteId);
        
        var webSite = await _webSiteRepository.GetById(webSiteId,cancellationToken);
        if(webSite.IsFailure)
            return webSite.Error.ToErrorList();
        
        _webSiteRepository.Delete(webSite.Value, cancellationToken);

        await _webSiteUnitOfWork.SaveChangesAsync(cancellationToken);
        return UnitResult.Success<ErrorList>();
    }
}