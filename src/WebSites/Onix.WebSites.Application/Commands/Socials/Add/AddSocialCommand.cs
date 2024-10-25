using Onix.Core.Abstraction;

namespace Onix.WebSites.Application.Commands.Socials.Add;

public record AddSocialCommand(
    Guid WebSiteId,
    string Social,
    string Link) : ICommand;