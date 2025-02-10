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
using Onix.WebSites.Domain.WebSites;

namespace Onix.WebSites.Application.Commands.WebSites.Create;

public class CreateWebSiteHandler
{
    private readonly ILogger<CreateWebSiteHandler> _logger;
    private readonly GetWebSiteByUrlHandler _getWebSiteByUrlHandler;
    private readonly IValidator<CreateWebSiteCommand> _validator;
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWebSiteHandler(
        IValidator<CreateWebSiteCommand> validator,
        IWebSiteRepository webSiteWebSiteRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateWebSiteHandler> logger,
        GetWebSiteByUrlHandler getWebSiteByUrlHandler)
    {
        _logger = logger;
        _getWebSiteByUrlHandler = getWebSiteByUrlHandler;
        _validator = validator;
        _webSiteRepository = webSiteWebSiteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateWebSiteCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var url = Domain.WebSites.ValueObjects.Domain.Create(command.Url).Value;
        var query = new GetWebSiteByUrlQuery(url.Value);
        
        var website = await _getWebSiteByUrlHandler.Handle(query,cancellationToken);
        if (website.IsSuccess)
            return Errors.Domains.AlreadyExist(nameof(url)).ToErrorList();

        var webSiteId = WebSiteId.NewId();
        var name = Name.Create(command.Name).Value;
        
        var webSiteToCreate = WebSite.Create(
            webSiteId,
            url,
            name,
            DateTime.UtcNow).Value;

        await _webSiteRepository.Add(webSiteToCreate, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return webSiteToCreate.Id.Value;
    }
}