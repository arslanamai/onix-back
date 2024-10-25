using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Commands.FAQs.Add;

public record AddFaqCommand(
    Guid WebSiteId,
    string Question,
    string Answer) : ICommand;