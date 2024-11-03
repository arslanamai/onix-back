using Onix.Core.Abstraction;
using Onix.WebSites.Domain.WebSites.ValueObjects;

namespace Onix.WebSites.Application.Commands.WebSites.AddFaq;

public record AddFaqCommand(
    Guid WebSiteId,
    List<Faq> Faqs) : ICommand;