using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Commands.WebSites.UpdateContact;

public record UpdateContactCommand(
    Guid WebSiteId,
    string Phone,
    string Email) : ICommand;