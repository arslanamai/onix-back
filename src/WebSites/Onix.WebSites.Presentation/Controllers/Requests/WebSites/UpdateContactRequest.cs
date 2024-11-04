using Onix.WebSites.Application.Commands.WebSites.UpdateContact;

namespace Onix.WebSites.Presentation.Controllers.Requests.WebSites;

public record UpdateContactRequest(
    string Email,
    string Phone)
{
    public UpdateContactCommand ToCommand(Guid id)
        => new UpdateContactCommand(id, Email, Phone);
}