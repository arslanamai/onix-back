using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Commands.FAQs.Delete;

public record DeleteFaqCommand(
    Guid WebSiteId,
    string Question) : ICommand;