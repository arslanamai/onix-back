using Onix.WebSites.Application.Commands.WebSites.AddFaq;

namespace Onix.WebSites.Presentation.Controllers.Requests.WebSites;

public record AddFaqRequest(
    List<Faq> Faqs)
{
    public AddFaqCommand ToCommand(Guid id)
        => new AddFaqCommand(id, Faqs);
}