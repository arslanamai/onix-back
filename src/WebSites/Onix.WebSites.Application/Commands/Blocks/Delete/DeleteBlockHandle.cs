using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Onix.Core.Abstraction;
using Onix.Core.Extensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Application.Database;

namespace Onix.WebSites.Application.Commands.Blocks.Delete;

public class DeleteBlockHandle
{
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly ILogger<DeleteBlockHandle> _logger;
    private readonly IWebSiteUnitOfWork _webSiteUnitOfWork;
    private readonly IValidator<DeleteBlockCommand> _validator;

    public DeleteBlockHandle(
        IWebSiteRepository webSiteRepository,
        ILogger<DeleteBlockHandle> logger,
        IWebSiteUnitOfWork webSiteUnitOfWork,
        IValidator<DeleteBlockCommand> validator)
    {
        _webSiteRepository = webSiteRepository;
        _logger = logger;
        _webSiteUnitOfWork = webSiteUnitOfWork;
        _validator = validator;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(
        DeleteBlockCommand command ,CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command,cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var webSiteId = WebSiteId.Create(command.WebSiteId);
        
        var webSiteResult = await _webSiteRepository
            .GetByIdWithBlocks(webSiteId, cancellationToken);
        if (webSiteResult.IsFailure)
            return webSiteResult.Error.ToErrorList();

        var blockId = BlockId.Create(command.BlockId);
        
        var blockResult = webSiteResult.Value.Blocks
            .FirstOrDefault(b => b.Id == blockId);
        if (blockResult is null)
            return Errors.General.NotFound(ConstType.Block).ToErrorList();

        var result = webSiteResult.Value.RemoveBlock(blockResult);
        if (result.IsFailure)
            return result.Error.ToErrorList();
        
        await _webSiteUnitOfWork.SaveChangesAsync(cancellationToken);
        return UnitResult.Success<ErrorList>();
    }
}