using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;
using Onix.WebSites.Domain.Blocks;

namespace Onix.WebSites.Application.Commands.Blocks.Add;

public class AddBlockHandler
{
    private readonly IValidator<AddBlockCommand> _validator;
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IWebSiteUnitOfWork _webSiteUnitOfWork;
    private readonly ILogger<AddBlockHandler> _logger;

    public AddBlockHandler(
        IValidator<AddBlockCommand> validator,
        IWebSiteRepository webSiteRepository,
        IWebSiteUnitOfWork webSiteUnitOfWork,
        ILogger<AddBlockHandler> logger)
    {
        _validator = validator;
        _webSiteRepository = webSiteRepository;
        _webSiteUnitOfWork = webSiteUnitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddBlockCommand command ,CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command,cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var webSiteId = WebSiteId.Create(command.WebSiteId);
        
        var webSiteResult = await _webSiteRepository
            .GetByIdWithBlocks(webSiteId, cancellationToken);
        if (webSiteResult.IsFailure)
            return webSiteResult.Error.ToErrorList();

        var blockId = BlockId.NewId();
        var code = Code.Create(command.Code).Value;
        var block = Block.Create(blockId, code).Value;

        var result = webSiteResult.Value.AddBlock(block);
        if (result.IsFailure)
            return result.Error.ToErrorList();

        await _webSiteUnitOfWork.SaveChangesAsync(cancellationToken);
        return block.Id.Value;
    }
}