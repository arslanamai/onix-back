using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;
using Onix.WebSites.Domain.WebSites.ValueObjects;

namespace Onix.WebSites.Application.Commands.WebSites.AddFaq;

public class AddFaqHandler
{
    private readonly ILogger<AddFaqHandler> _logger;
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddFaqCommand> _validator;

    public AddFaqHandler(
        ILogger<AddFaqHandler> logger,
        IWebSiteRepository webSiteRepository,
        IUnitOfWork unitOfWork,
        IValidator<AddFaqCommand> validator)
    {
        _logger = logger;
        _webSiteRepository = webSiteRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddFaqCommand command, CancellationToken cancellationToken = default)
    {
        var validator = await _validator.ValidateAsync(command, cancellationToken);
        if (!validator.IsValid)
            return validator.ToList();

        var webSiteId = WebSiteId.Create(command.WebSiteId);

        var webSiteResult = await _webSiteRepository
            .GetById(webSiteId, cancellationToken);
        if (webSiteResult.IsFailure)
            return webSiteResult.Error.ToErrorList();

        var result = webSiteResult.Value.UpdateFAQs(command.Faqs);
        if (result.IsFailure)
            return result.Error.ToErrorList();
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return webSiteResult.Value.Id.Value;
    }
}