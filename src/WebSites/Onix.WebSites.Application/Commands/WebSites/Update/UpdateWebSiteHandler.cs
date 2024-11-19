using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;
using Onix.WebSites.Application.Queries.WebSites.GetByUrl;
using Onix.WebSites.Domain.WebSites.ValueObjects;

namespace Onix.WebSites.Application.Commands.WebSites.Update;

public class UpdateWebSiteHandler
{
    private readonly IValidator<UpdateWebSiteCommand> _validator;
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly GetWebSiteByUrlHandler _getWebSiteByUrlHandler;
    private readonly ILogger<UpdateWebSiteHandler> _logger;

    public UpdateWebSiteHandler(
        IValidator<UpdateWebSiteCommand> validator,
        IWebSiteRepository webSiteRepository,
        IUnitOfWork unitOfWork,
        GetWebSiteByUrlHandler getWebSiteByUrlHandler,
        ILogger<UpdateWebSiteHandler> logger)
    {
        _validator = validator;
        _webSiteRepository = webSiteRepository;
        _unitOfWork = unitOfWork;
        _getWebSiteByUrlHandler = getWebSiteByUrlHandler;
        _logger = logger;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(
        UpdateWebSiteCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var url = Url.Create(command.Url).Value;
        var query = new GetWebSiteByUrlQuery(url.Value);
        
        var existingWebsite = await _getWebSiteByUrlHandler.Handle(query, cancellationToken);
        if (existingWebsite.IsSuccess)
            return Errors.Domain.AlreadyExist(nameof(url)).ToErrorList();

        var webSiteId = WebSiteId.Create(command.WebSiteId);
        
        var webSiteResult = await _webSiteRepository.GetById(webSiteId, cancellationToken);
        if (webSiteResult.IsFailure)
            return webSiteResult.Error.ToErrorList();

        var name = Name.Create(command.Name).Value;
        
        var result = webSiteResult.Value.Update(url, name);
        if (result.IsFailure)
            return result.Error.ToErrorList();
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return UnitResult.Success<ErrorList>();
    }
}