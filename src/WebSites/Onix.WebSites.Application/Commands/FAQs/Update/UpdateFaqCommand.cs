using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Commands.FAQs.Update;

public record UpdateFaqCommand(
    Guid WebSiteId,
    string Question,
    string Answer) : ICommand;