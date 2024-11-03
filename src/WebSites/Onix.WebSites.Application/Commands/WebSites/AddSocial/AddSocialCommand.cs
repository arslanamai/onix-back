using Onix.Core.Abstraction;
using Onix.WebSites.Domain.WebSites.ValueObjects;

namespace Onix.WebSites.Application.Commands.WebSites.AddSocial;

public record AddSocialCommand(
    Guid WebSiteId,
    List<SocialMedia> SocialMedia) : ICommand;